using RpxCodeGenerator.Core.Generators;
using RpxCodeGenerator.Core.Models;
using RpxCodeGenerator.Core.Parsers;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeGenerator = RpxCodeGenerator.Core.Generators.CodeGenerator;

namespace RpxCodeGenerator.WinForms;

public partial class MainForm : Form
{
	private readonly RpxParser _parser = new();
	private readonly CodeGenerator _codeGenerator = new();

	private readonly List<string> _loadedFiles = [];
	private readonly List<string> _loadedFolders = [];
	private readonly Dictionary<string, RpxDocument> _parsedDocuments = [];
	private readonly Dictionary<string, GeneratedOutput> _generatedOutputs = [];
	private static readonly Icon? AppIcon = LoadAppIcon();

	public MainForm()
	{
		InitializeComponent();
		if (AppIcon != null)
			Icon = AppIcon;
		this.DragEnter += MainForm_DragEnter;
		this.DragDrop += MainForm_DragDrop;
	}

	private static Icon? LoadAppIcon()
	{
		try
		{
			using var iconStream = new MemoryStream(AppIconBase64.ToBytes());
			return new Icon(iconStream);
		}
		catch
		{
			
			return null;
		}
	}


	// ────────────────── Open Files ──────────────────

	private void BtnOpenFiles_Click(object? sender, EventArgs e)
	{
		using var dlg = new OpenFileDialog
		{
			Title = "Select RPX Files",
			Filter = "RPX Files (*.rpx)|*.rpx|All Files (*.*)|*.*",
			Multiselect = true
		};

		if (dlg.ShowDialog() == DialogResult.OK)
		{
			AddFiles(dlg.FileNames);
		}
	}

	private void BtnOpenFolder_Click(object? sender, EventArgs e)
	{
		using var dlg = new FolderBrowserDialog
		{
			Description = "Select folder containing RPX files",
			UseDescriptionForTitle = true
		};

		if (dlg.ShowDialog() == DialogResult.OK)
		{
			var files = Directory.GetFiles(dlg.SelectedPath, "*.rpx");
			if (files.Length == 0)
			{
				MessageBox.Show("No RPX files found in the selected folder.",
					"No Files", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			if (!_loadedFolders.Contains(dlg.SelectedPath))
				_loadedFolders.Add(dlg.SelectedPath);
			AddFiles(files);
		}
	}

	// ────────────────── Drag & Drop ──────────────────

	private void MainForm_DragEnter(object? sender, DragEventArgs e)
	{
		if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
			e.Effect = DragDropEffects.Copy;
	}

	private void MainForm_DragDrop(object? sender, DragEventArgs e)
	{
		if (e.Data?.GetData(DataFormats.FileDrop) is not string[] paths)
			return;

		var rpxFiles = new List<string>();
		foreach (var path in paths)
		{
			if (Directory.Exists(path))
			{
				rpxFiles.AddRange(Directory.GetFiles(path, "*.rpx"));
			}
			else if (File.Exists(path) && path.EndsWith(".rpx", StringComparison.OrdinalIgnoreCase))
			{
				rpxFiles.Add(path);
			}
		}

		if (rpxFiles.Count > 0)
			AddFiles([.. rpxFiles]);
		else
			MessageBox.Show("No RPX files found in the dropped items.",
				"No Files", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}

	// ────────────────── Core Logic ──────────────────

	private void AddFiles(string[] files)
	{
		var added = 0;
		foreach (var file in files.OrderBy(f => Path.GetFileName(f)))
		{
			if (_loadedFiles.Contains(file))
				continue;

			_loadedFiles.Add(file);
			lstFiles.Items.Add(Path.GetFileName(file));
			added++;
		}

		UpdateUI();
		lblStatus.Text = $"Added {added} file(s). Total: {_loadedFiles.Count}";

		if (_loadedFiles.Count > 0 && lstFiles.SelectedIndex < 0)
			lstFiles.SelectedIndex = 0;
	}

	private void BtnGenerate_Click(object? sender, EventArgs e)
	{
		if (_loadedFiles.Count == 0)
			return;

		progressBar.Visible = true;
		progressBar.Maximum = _loadedFiles.Count;
		progressBar.Value = 0;

		_parsedDocuments.Clear();
		_generatedOutputs.Clear();

		var errors = new List<string>();

		foreach (var file in _loadedFiles)
		{
			try
			{
				var rpxDoc = _parser.ParseFile(file);
				_parsedDocuments[file] = rpxDoc;

				var output = new GeneratedOutput
				{
					InitCode = _codeGenerator.Generate(rpxDoc),
					ControlsCode = _codeGenerator.GenerateTypedControlsExtraction(rpxDoc),
					ControlsCode2 = _codeGenerator.GenerateTypedControlsExtraction2(rpxDoc),
					Summary = _codeGenerator.GenerateReportSummary(rpxDoc)
				};
				_generatedOutputs[file] = output;
			}
			catch (Exception ex)
			{
				errors.Add($"{Path.GetFileName(file)}: {ex.Message}");
			}

			progressBar.Value++;
		}

		progressBar.Visible = false;

		// Show result for currently selected file
		ShowSelectedFileOutput();

		var msg = $"Generated code for {_generatedOutputs.Count}/{_loadedFiles.Count} file(s).";
		if (errors.Count > 0)
			msg += $" ({errors.Count} error(s))";

		lblStatus.Text = msg;
		btnSaveAll.Enabled = _generatedOutputs.Count > 0;

		if (errors.Count > 0)
		{
			MessageBox.Show(
				string.Join(Environment.NewLine, errors),
				"Generation Errors",
				MessageBoxButtons.OK,
				MessageBoxIcon.Warning);
		}
	}

	private void BtnSaveAll_Click(object? sender, EventArgs e)
	{
		if (_generatedOutputs.Count == 0)
			return;

		using var dlg = new FolderBrowserDialog
		{
			Description = "Select output folder for generated code",
			UseDescriptionForTitle = true
		};

		if (dlg.ShowDialog() != DialogResult.OK)
			return;

		var outputDir = dlg.SelectedPath;
		var saved = 0;

		foreach (var kvp in _generatedOutputs)
		{
			var baseName = Path.GetFileNameWithoutExtension(kvp.Key);
			var output = kvp.Value;

			File.WriteAllText(Path.Combine(outputDir, $"{baseName}_Initialize.cs"), output.InitCode);
			File.WriteAllText(Path.Combine(outputDir, $"{baseName}_Controls.cs"), output.ControlsCode);
			File.WriteAllText(Path.Combine(outputDir, $"{baseName}_Controls2.cs"), output.ControlsCode2);
			File.WriteAllText(Path.Combine(outputDir, $"{baseName}_Summary.txt"), output.Summary);
			saved++;
		}

		lblStatus.Text = $"Saved {saved} file(s) to: {outputDir}";
		MessageBox.Show(
			$"Successfully saved {saved * 3} files to:\n{outputDir}",
			"Save Complete",
			MessageBoxButtons.OK,
			MessageBoxIcon.Information);
	}

	private void BtnClearAll_Click(object? sender, EventArgs e)
	{
		_loadedFiles.Clear();
		_loadedFolders.Clear();
		_parsedDocuments.Clear();
		_generatedOutputs.Clear();
		lstFiles.Items.Clear();
		txtInitCode.Clear();
		txtControlsCode.Clear();
		txtControlsCode2.Clear();
		txtSummary.Clear();
		UpdateUI();
		lblStatus.Text = "Cleared. Open RPX file(s) or folder to start.";
	}

	private void LstFiles_SelectedIndexChanged(object? sender, EventArgs e)
	{
		ShowSelectedFileOutput();
	}

	// ────────────────── Helpers ──────────────────

	private void ShowSelectedFileOutput()
	{
		if (lstFiles.SelectedIndex < 0 || lstFiles.SelectedIndex >= _loadedFiles.Count)
		{
			txtInitCode.Clear();
			txtControlsCode.Clear();
			txtControlsCode2.Clear();
			txtSummary.Clear();
			return;
		}

		var file = _loadedFiles[lstFiles.SelectedIndex];

		if (_generatedOutputs.TryGetValue(file, out var output))
		{
			txtInitCode.Text = output.InitCode;
			txtControlsCode.Text = output.ControlsCode;
			txtControlsCode2.Text = output.ControlsCode2;
			txtSummary.Text = output.Summary;
		}
		else
		{
			txtInitCode.Text = "// Click \"⚡ Generate All\" to generate code.";
			txtControlsCode.Text = "// Click \"⚡ Generate All\" to generate code.";
			txtControlsCode2.Text = "// Click \"⚡ Generate All\" to generate code.";
			txtSummary.Text = "// Click \"⚡ Generate All\" to generate code.";
		}
	}

	private void UpdateUI()
	{
		btnGenerate.Enabled = _loadedFiles.Count > 0;
		btnSaveAll.Enabled = _generatedOutputs.Count > 0;
		lblFileCount.Text = $"Files: {_loadedFiles.Count}";
	}

	private void ExitMenuItem_Click(object s, EventArgs e)
	{
		Close();
	}

	// ────────────────── Refresh ──────────────────

	private void BtnRefresh_Click(object? sender, EventArgs e)
	{
		RefreshFolders();
	}

	private void RefreshFolders()
	{
		if (_loadedFolders.Count == 0)
		{
			lblStatus.Text = "No folders loaded. Open a folder first.";
			return;
		}

		var newFiles = new List<string>();
		var removedCount = 0;

		// Xóa file không còn tồn tại
		var missing = _loadedFiles.Where(f => !File.Exists(f)).ToList();
		foreach (var f in missing)
		{
			var idx = _loadedFiles.IndexOf(f);
			_loadedFiles.RemoveAt(idx);
			lstFiles.Items.RemoveAt(idx);
			_parsedDocuments.Remove(f);
			_generatedOutputs.Remove(f);
			removedCount++;
		}

		// Quét lại thư mục tìm file mới
		foreach (var folder in _loadedFolders)
		{
			if (!Directory.Exists(folder))
				continue;
			foreach (var file in Directory.GetFiles(folder, "*.rpx").OrderBy(Path.GetFileName))
			{
				if (!_loadedFiles.Contains(file))
					newFiles.Add(file);
			}
		}

		if (newFiles.Count > 0)
			AddFiles([.. newFiles]);

		UpdateUI();
		lblStatus.Text = $"Refreshed: +{newFiles.Count} new, -{removedCount} removed. Total: {_loadedFiles.Count}";
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == Keys.F5)
		{
			RefreshFolders();
			return true;
		}
		return base.ProcessCmdKey(ref msg, keyData);
	}

	// ────────────────── Inner Types ──────────────────

	private sealed class GeneratedOutput
	{
		public string InitCode { get; set; } = string.Empty;
		public string ControlsCode { get; set; } = string.Empty;
		public string ControlsCode2 { get; set; } = string.Empty;
		public string Summary { get; set; } = string.Empty;
	}
}
