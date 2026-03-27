using System.Reflection.Emit;
using Label = System.Windows.Forms.Label;

namespace RpxCodeGenerator.WinForms;

partial class MainForm
{
	private System.ComponentModel.IContainer components = null;

	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	private void InitializeComponent()
	{
		this.menuStrip = new MenuStrip();
		this.fileMenu = new ToolStripMenuItem();
		this.openFilesMenuItem = new ToolStripMenuItem();
		this.openFolderMenuItem = new ToolStripMenuItem();
		this.toolStripSeparator1 = new ToolStripSeparator();
		this.exitMenuItem = new ToolStripMenuItem();

		this.toolStrip = new ToolStrip();
		this.btnOpenFiles = new ToolStripButton();
		this.btnOpenFolder = new ToolStripButton();
		this.toolStripSeparator2 = new ToolStripSeparator();
		this.btnGenerate = new ToolStripButton();
		this.btnSaveAll = new ToolStripButton();
		this.toolStripSeparator3 = new ToolStripSeparator();
		this.btnClearAll = new ToolStripButton();

		this.splitContainerMain = new SplitContainer();
		this.lstFiles = new ListBox();
		this.lblFileList = new Label();

		this.tabControl = new TabControl();
		this.tabInitCode = new TabPage();
		this.txtInitCode = new RichTextBox();
		this.tabControlsCode = new TabPage();
		this.tabControlsCode2 = new TabPage();
		this.txtControlsCode = new RichTextBox();
		this.txtControlsCode2 = new RichTextBox();
		this.tabSummary = new TabPage();
		this.txtSummary = new RichTextBox();

		this.statusStrip = new StatusStrip();
		this.lblStatus = new ToolStripStatusLabel();
		this.lblFileCount = new ToolStripStatusLabel();
		this.progressBar = new ToolStripProgressBar();

		// menuStrip
		this.menuStrip.Items.AddRange([this.fileMenu]);
		this.menuStrip.Location = new Point(0, 0);
		this.menuStrip.Name = "menuStrip";
		this.menuStrip.Size = new Size(1000, 24);
		this.menuStrip.TabIndex = 0;

		// fileMenu
		this.fileMenu.DropDownItems.AddRange([
			this.openFilesMenuItem,
			this.openFolderMenuItem,
			this.toolStripSeparator1,
			this.exitMenuItem
		]);
		this.fileMenu.Name = "fileMenu";
		this.fileMenu.Text = "&File";

		// openFilesMenuItem
		this.openFilesMenuItem.Name = "openFilesMenuItem";
		this.openFilesMenuItem.ShortcutKeys = Keys.Control | Keys.O;
		this.openFilesMenuItem.Text = "Open RPX File(s)...";
		this.openFilesMenuItem.Click += BtnOpenFiles_Click;

		// openFolderMenuItem
		this.openFolderMenuItem.Name = "openFolderMenuItem";
		this.openFolderMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.O;
		this.openFolderMenuItem.Text = "Open RPX Folder...";
		this.openFolderMenuItem.Click += BtnOpenFolder_Click;

		// exitMenuItem
		this.exitMenuItem.Name = "exitMenuItem";
		this.exitMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
		this.exitMenuItem.Text = "Exit";
		this.exitMenuItem.Click += (s, e) => Close();

		// toolStrip
		this.toolStrip.Items.AddRange([
			this.btnOpenFiles,
			this.btnOpenFolder,
			this.toolStripSeparator2,
			this.btnGenerate,
			this.btnSaveAll,
			this.toolStripSeparator3,
			this.btnClearAll
		]);
		this.toolStrip.Location = new Point(0, 24);
		this.toolStrip.Name = "toolStrip";
		this.toolStrip.Size = new Size(1000, 25);
		this.toolStrip.TabIndex = 1;

		// btnOpenFiles
		this.btnOpenFiles.Name = "btnOpenFiles";
		this.btnOpenFiles.Text = "📂 Open File(s)";
		this.btnOpenFiles.Click += BtnOpenFiles_Click;

		// btnOpenFolder
		this.btnOpenFolder.Name = "btnOpenFolder";
		this.btnOpenFolder.Text = "📁 Open Folder";
		this.btnOpenFolder.Click += BtnOpenFolder_Click;

		// btnGenerate
		this.btnGenerate.Name = "btnGenerate";
		this.btnGenerate.Text = "⚡ Generate All";
		this.btnGenerate.Enabled = false;
		this.btnGenerate.Click += BtnGenerate_Click;

		// btnSaveAll
		this.btnSaveAll.Name = "btnSaveAll";
		this.btnSaveAll.Text = "💾 Save All";
		this.btnSaveAll.Enabled = false;
		this.btnSaveAll.Click += BtnSaveAll_Click;

		// btnClearAll
		this.btnClearAll.Name = "btnClearAll";
		this.btnClearAll.Text = "🗑 Clear";
		this.btnClearAll.Click += BtnClearAll_Click;

		// splitContainerMain
		this.splitContainerMain.Dock = DockStyle.Fill;
		this.splitContainerMain.Location = new Point(0, 49);
		this.splitContainerMain.Name = "splitContainerMain";
		this.splitContainerMain.SplitterDistance = 260;

		// Left panel - file list
		this.lblFileList.Text = "  RPX Files:";
		this.lblFileList.Dock = DockStyle.Top;
		this.lblFileList.Height = 24;
		this.lblFileList.TextAlign = ContentAlignment.MiddleLeft;
		this.lblFileList.BackColor = SystemColors.ControlDark;
		this.lblFileList.ForeColor = SystemColors.ControlLightLight;
		this.lblFileList.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

		this.lstFiles.Dock = DockStyle.Fill;
		this.lstFiles.Name = "lstFiles";
		this.lstFiles.IntegralHeight = false;
		this.lstFiles.Font = new Font("Consolas", 9F);
		this.lstFiles.SelectedIndexChanged += LstFiles_SelectedIndexChanged;

		this.splitContainerMain.Panel1.Controls.Add(this.lstFiles);
		this.splitContainerMain.Panel1.Controls.Add(this.lblFileList);
		this.splitContainerMain.Panel1MinSize = 180;

		// Right panel - tabs
		this.tabControl.Dock = DockStyle.Fill;
		this.tabControl.Name = "tabControl";

		// tabInitCode
		this.tabInitCode.Text = "Initialize Code";
		this.tabInitCode.Padding = new Padding(3);
		this.txtInitCode.Dock = DockStyle.Fill;
		this.txtInitCode.Font = new Font("Consolas", 10F);
		this.txtInitCode.ReadOnly = true;
		this.txtInitCode.WordWrap = false;
		this.txtInitCode.BackColor = Color.FromArgb(30, 30, 30);
		this.txtInitCode.ForeColor = Color.FromArgb(212, 212, 212);
		this.tabInitCode.Controls.Add(this.txtInitCode);

		// tabControlsCode
		this.tabControlsCode.Text = "Controls Code";
		this.tabControlsCode.Padding = new Padding(3);
		this.txtControlsCode.Dock = DockStyle.Fill;
		this.txtControlsCode.Font = new Font("Consolas", 10F);
		this.txtControlsCode.ReadOnly = true;
		this.txtControlsCode.WordWrap = false;
		this.txtControlsCode.BackColor = Color.FromArgb(30, 30, 30);
		this.txtControlsCode.ForeColor = Color.FromArgb(212, 212, 212);
		this.tabControlsCode.Controls.Add(this.txtControlsCode);

		// tabControlsCode
		this.tabControlsCode2.Text = "Controls Code2";
		this.tabControlsCode2.Padding = new Padding(3);
		this.txtControlsCode2.Dock = DockStyle.Fill;
		this.txtControlsCode2.Font = new Font("Consolas", 10F);
		this.txtControlsCode2.ReadOnly = true;
		this.txtControlsCode2.WordWrap = false;
		this.txtControlsCode2.BackColor = Color.FromArgb(30, 30, 30);
		this.txtControlsCode2.ForeColor = Color.FromArgb(212, 212, 212);
		this.tabControlsCode2.Controls.Add(this.txtControlsCode2);

		// tabSummary
		this.tabSummary.Text = "Summary";
		this.tabSummary.Padding = new Padding(3);
		this.txtSummary.Dock = DockStyle.Fill;
		this.txtSummary.Font = new Font("Consolas", 10F);
		this.txtSummary.ReadOnly = true;
		this.txtSummary.WordWrap = false;
		this.txtSummary.BackColor = Color.FromArgb(30, 30, 30);
		this.txtSummary.ForeColor = Color.FromArgb(212, 212, 212);
		this.tabSummary.Controls.Add(this.txtSummary);

		this.tabControl.TabPages.AddRange([this.tabInitCode, this.tabControlsCode, this.tabControlsCode2, this.tabSummary]);
		this.splitContainerMain.Panel2.Controls.Add(this.tabControl);

		// statusStrip
		this.statusStrip.Items.AddRange([this.lblStatus, this.progressBar, this.lblFileCount]);
		this.statusStrip.Name = "statusStrip";

		this.lblStatus.Name = "lblStatus";
		this.lblStatus.Text = "Ready. Open RPX file(s) or folder to start.";
		this.lblStatus.Spring = true;
		this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;

		this.progressBar.Name = "progressBar";
		this.progressBar.Size = new Size(150, 16);
		this.progressBar.Visible = false;

		this.lblFileCount.Name = "lblFileCount";
		this.lblFileCount.Text = "Files: 0";
		this.lblFileCount.BorderSides = ToolStripStatusLabelBorderSides.Left;

		// MainForm
		this.AutoScaleDimensions = new SizeF(7F, 15F);
		this.AutoScaleMode = AutoScaleMode.Font;
		this.ClientSize = new Size(1100, 700);
		this.Controls.Add(this.splitContainerMain);
		this.Controls.Add(this.toolStrip);
		this.Controls.Add(this.menuStrip);
		this.Controls.Add(this.statusStrip);
		this.MainMenuStrip = this.menuStrip;
		this.Name = "MainForm";
		this.Text = "RPX to C# Code Generator";
		this.StartPosition = FormStartPosition.CenterScreen;
		this.AllowDrop = true;

		((System.ComponentModel.ISupportInitialize)this.splitContainerMain).BeginInit();
		this.splitContainerMain.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainerMain).EndInit();
		this.ResumeLayout(false);
		this.PerformLayout();
	}

	#endregion

	private MenuStrip menuStrip;
	private ToolStripMenuItem fileMenu;
	private ToolStripMenuItem openFilesMenuItem;
	private ToolStripMenuItem openFolderMenuItem;
	private ToolStripSeparator toolStripSeparator1;
	private ToolStripMenuItem exitMenuItem;

	private ToolStrip toolStrip;
	private ToolStripButton btnOpenFiles;
	private ToolStripButton btnOpenFolder;
	private ToolStripSeparator toolStripSeparator2;
	private ToolStripButton btnGenerate;
	private ToolStripButton btnSaveAll;
	private ToolStripSeparator toolStripSeparator3;
	private ToolStripButton btnClearAll;

	private SplitContainer splitContainerMain;
	private ListBox lstFiles;
	private Label lblFileList;

	private TabControl tabControl;
	private TabPage tabInitCode;
	private RichTextBox txtInitCode;
	private TabPage tabControlsCode;
	private TabPage tabControlsCode2;
	private RichTextBox txtControlsCode;
	private RichTextBox txtControlsCode2;
	private TabPage tabSummary;
	private RichTextBox txtSummary;

	private StatusStrip statusStrip;
	private ToolStripStatusLabel lblStatus;
	private ToolStripStatusLabel lblFileCount;
	private ToolStripProgressBar progressBar;
}
