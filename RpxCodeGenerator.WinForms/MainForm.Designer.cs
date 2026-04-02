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
		menuStrip = new MenuStrip();
		fileMenu = new ToolStripMenuItem();
		openFilesMenuItem = new ToolStripMenuItem();
		openFolderMenuItem = new ToolStripMenuItem();
		refreshMenuItem = new ToolStripMenuItem();
		toolStripSeparator1 = new ToolStripSeparator();
		exitMenuItem = new ToolStripMenuItem();
		toolStrip = new ToolStrip();
		btnOpenFiles = new ToolStripButton();
		btnOpenFolder = new ToolStripButton();
		btnRefresh = new ToolStripButton();
		toolStripSeparator2 = new ToolStripSeparator();
		btnGenerate = new ToolStripButton();
		btnSaveAll = new ToolStripButton();
		toolStripSeparator3 = new ToolStripSeparator();
		btnClearAll = new ToolStripButton();
		splitContainerMain = new SplitContainer();
		lstFiles = new ListBox();
		lblFileList = new Label();
		tabControl = new TabControl();
		tabInitCode = new TabPage();
		txtInitCode = new RichTextBox();
		tabControlsCode = new TabPage();
		txtControlsCode = new RichTextBox();
		tabControlsCode2 = new TabPage();
		txtControlsCode2 = new RichTextBox();
		tabSummary = new TabPage();
		txtSummary = new RichTextBox();
		statusStrip = new StatusStrip();
		lblStatus = new ToolStripStatusLabel();
		progressBar = new ToolStripProgressBar();
		lblFileCount = new ToolStripStatusLabel();
		menuStrip.SuspendLayout();
		toolStrip.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
		splitContainerMain.Panel1.SuspendLayout();
		splitContainerMain.Panel2.SuspendLayout();
		splitContainerMain.SuspendLayout();
		tabControl.SuspendLayout();
		tabInitCode.SuspendLayout();
		tabControlsCode.SuspendLayout();
		tabControlsCode2.SuspendLayout();
		tabSummary.SuspendLayout();
		statusStrip.SuspendLayout();
		SuspendLayout();
		// 
		// menuStrip
		// 
		menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu });
		menuStrip.Location = new Point(0, 0);
		menuStrip.Name = "menuStrip";
		menuStrip.Size = new Size(1100, 24);
		menuStrip.TabIndex = 0;
		// 
		// fileMenu
		// 
		fileMenu.DropDownItems.AddRange(new ToolStripItem[] { openFilesMenuItem, openFolderMenuItem, refreshMenuItem, toolStripSeparator1, exitMenuItem });
		fileMenu.Name = "fileMenu";
		fileMenu.Size = new Size(37, 20);
		fileMenu.Text = "&File";
		// 
		// openFilesMenuItem
		// 
		openFilesMenuItem.Name = "openFilesMenuItem";
		openFilesMenuItem.ShortcutKeys = Keys.Control | Keys.O;
		openFilesMenuItem.Size = new Size(245, 22);
		openFilesMenuItem.Text = "Open RPX File(s)...";
		openFilesMenuItem.Click += BtnOpenFiles_Click;
		// 
		// openFolderMenuItem
		// 
		openFolderMenuItem.Name = "openFolderMenuItem";
		openFolderMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.O;
		openFolderMenuItem.Size = new Size(245, 22);
		openFolderMenuItem.Text = "Open RPX Folder...";
		openFolderMenuItem.Click += BtnOpenFolder_Click;
		// 
		// refreshMenuItem
		// 
		refreshMenuItem.Name = "refreshMenuItem";
		refreshMenuItem.ShortcutKeys = Keys.F5;
		refreshMenuItem.Size = new Size(245, 22);
		refreshMenuItem.Text = "Refresh Folder(s)";
		refreshMenuItem.Click += BtnRefresh_Click;
		// 
		// toolStripSeparator1
		// 
		toolStripSeparator1.Name = "toolStripSeparator1";
		toolStripSeparator1.Size = new Size(242, 6);
		// 
		// exitMenuItem
		// 
		exitMenuItem.Name = "exitMenuItem";
		exitMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
		exitMenuItem.Size = new Size(245, 22);
		exitMenuItem.Text = "Exit";
		exitMenuItem.Click += ExitMenuItem_Click;
		// 
		// toolStrip
		// 
		toolStrip.Items.AddRange(new ToolStripItem[] { btnOpenFiles, btnOpenFolder, btnRefresh, toolStripSeparator2, btnGenerate, btnSaveAll, toolStripSeparator3, btnClearAll });
		toolStrip.Location = new Point(0, 24);
		toolStrip.Name = "toolStrip";
		toolStrip.Size = new Size(1100, 25);
		toolStrip.TabIndex = 1;
		// 
		// btnOpenFiles
		// 
		btnOpenFiles.Name = "btnOpenFiles";
		btnOpenFiles.Size = new Size(89, 22);
		btnOpenFiles.Text = "📂 Open File(s)";
		btnOpenFiles.Click += BtnOpenFiles_Click;
		// 
		// btnOpenFolder
		// 
		btnOpenFolder.Name = "btnOpenFolder";
		btnOpenFolder.Size = new Size(91, 22);
		btnOpenFolder.Text = "📁 Open Folder";
		btnOpenFolder.Click += BtnOpenFolder_Click;
		// 
		// btnRefresh
		// 
		btnRefresh.Name = "btnRefresh";
		btnRefresh.Text = "🔄 Refresh (F5)";
		btnRefresh.Click += BtnRefresh_Click;
		// 
		// toolStripSeparator2
		// 
		toolStripSeparator2.Name = "toolStripSeparator2";
		toolStripSeparator2.Size = new Size(6, 25);
		// 
		// btnGenerate
		// 
		btnGenerate.Enabled = false;
		btnGenerate.Name = "btnGenerate";
		btnGenerate.Size = new Size(90, 22);
		btnGenerate.Text = "⚡ Generate All";
		btnGenerate.Click += BtnGenerate_Click;
		// 
		// btnSaveAll
		// 
		btnSaveAll.Enabled = false;
		btnSaveAll.Name = "btnSaveAll";
		btnSaveAll.Size = new Size(67, 22);
		btnSaveAll.Text = "💾 Save All";
		btnSaveAll.Click += BtnSaveAll_Click;
		// 
		// toolStripSeparator3
		// 
		toolStripSeparator3.Name = "toolStripSeparator3";
		toolStripSeparator3.Size = new Size(6, 25);
		// 
		// btnClearAll
		// 
		btnClearAll.Name = "btnClearAll";
		btnClearAll.Size = new Size(52, 22);
		btnClearAll.Text = "🗑 Clear";
		btnClearAll.Click += BtnClearAll_Click;
		// 
		// splitContainerMain
		// 
		splitContainerMain.Dock = DockStyle.Fill;
		splitContainerMain.Location = new Point(0, 49);
		splitContainerMain.Name = "splitContainerMain";
		// 
		// splitContainerMain.Panel1
		// 
		splitContainerMain.Panel1.Controls.Add(lstFiles);
		splitContainerMain.Panel1.Controls.Add(lblFileList);
		splitContainerMain.Panel1MinSize = 180;
		// 
		// splitContainerMain.Panel2
		// 
		splitContainerMain.Panel2.Controls.Add(tabControl);
		splitContainerMain.Size = new Size(1100, 627);
		splitContainerMain.SplitterDistance = 400;
		splitContainerMain.TabIndex = 0;
		// 
		// lstFiles
		// 
		lstFiles.Dock = DockStyle.Fill;
		lstFiles.Font = new Font("Consolas", 9F);
		lstFiles.IntegralHeight = false;
		lstFiles.Location = new Point(0, 24);
		lstFiles.Name = "lstFiles";
		lstFiles.Size = new Size(400, 603);
		lstFiles.TabIndex = 0;
		lstFiles.SelectedIndexChanged += LstFiles_SelectedIndexChanged;
		// 
		// lblFileList
		// 
		lblFileList.BackColor = SystemColors.ControlDark;
		lblFileList.Dock = DockStyle.Top;
		lblFileList.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
		lblFileList.ForeColor = SystemColors.ControlLightLight;
		lblFileList.Location = new Point(0, 0);
		lblFileList.Name = "lblFileList";
		lblFileList.Size = new Size(400, 24);
		lblFileList.TabIndex = 1;
		lblFileList.Text = "  RPX Files:";
		lblFileList.TextAlign = ContentAlignment.MiddleLeft;
		// 
		// tabControl
		// 
		tabControl.Controls.Add(tabInitCode);
		tabControl.Controls.Add(tabControlsCode);
		tabControl.Controls.Add(tabControlsCode2);
		tabControl.Controls.Add(tabSummary);
		tabControl.Dock = DockStyle.Fill;
		tabControl.Location = new Point(0, 0);
		tabControl.Name = "tabControl";
		tabControl.SelectedIndex = 0;
		tabControl.Size = new Size(696, 627);
		tabControl.TabIndex = 0;
		// 
		// tabInitCode
		// 
		tabInitCode.Controls.Add(txtInitCode);
		tabInitCode.Location = new Point(4, 24);
		tabInitCode.Name = "tabInitCode";
		tabInitCode.Padding = new Padding(3);
		tabInitCode.Size = new Size(688, 599);
		tabInitCode.TabIndex = 0;
		tabInitCode.Text = "Initialize Code";
		// 
		// txtInitCode
		// 
		txtInitCode.BackColor = Color.FromArgb(30, 30, 30);
		txtInitCode.Dock = DockStyle.Fill;
		txtInitCode.Font = new Font("Consolas", 10F);
		txtInitCode.ForeColor = Color.FromArgb(212, 212, 212);
		txtInitCode.Location = new Point(3, 3);
		txtInitCode.Name = "txtInitCode";
		txtInitCode.ReadOnly = true;
		txtInitCode.Size = new Size(682, 593);
		txtInitCode.TabIndex = 0;
		txtInitCode.Text = "";
		txtInitCode.WordWrap = false;
		// 
		// tabControlsCode
		// 
		tabControlsCode.Controls.Add(txtControlsCode);
		tabControlsCode.Location = new Point(4, 24);
		tabControlsCode.Name = "tabControlsCode";
		tabControlsCode.Padding = new Padding(3);
		tabControlsCode.Size = new Size(17, 72);
		tabControlsCode.TabIndex = 1;
		tabControlsCode.Text = "Controls Code";
		// 
		// txtControlsCode
		// 
		txtControlsCode.BackColor = Color.FromArgb(30, 30, 30);
		txtControlsCode.Dock = DockStyle.Fill;
		txtControlsCode.Font = new Font("Consolas", 10F);
		txtControlsCode.ForeColor = Color.FromArgb(212, 212, 212);
		txtControlsCode.Location = new Point(3, 3);
		txtControlsCode.Name = "txtControlsCode";
		txtControlsCode.ReadOnly = true;
		txtControlsCode.Size = new Size(11, 66);
		txtControlsCode.TabIndex = 0;
		txtControlsCode.Text = "";
		txtControlsCode.WordWrap = false;
		// 
		// tabControlsCode2
		// 
		tabControlsCode2.Controls.Add(txtControlsCode2);
		tabControlsCode2.Location = new Point(4, 24);
		tabControlsCode2.Name = "tabControlsCode2";
		tabControlsCode2.Padding = new Padding(3);
		tabControlsCode2.Size = new Size(688, 599);
		tabControlsCode2.TabIndex = 2;
		tabControlsCode2.Text = "Controls Code2";
		// 
		// txtControlsCode2
		// 
		txtControlsCode2.BackColor = Color.FromArgb(30, 30, 30);
		txtControlsCode2.Dock = DockStyle.Fill;
		txtControlsCode2.Font = new Font("Consolas", 10F);
		txtControlsCode2.ForeColor = Color.FromArgb(212, 212, 212);
		txtControlsCode2.Location = new Point(3, 3);
		txtControlsCode2.Name = "txtControlsCode2";
		txtControlsCode2.ReadOnly = true;
		txtControlsCode2.Size = new Size(682, 593);
		txtControlsCode2.TabIndex = 0;
		txtControlsCode2.Text = "";
		txtControlsCode2.WordWrap = false;
		// 
		// tabSummary
		// 
		tabSummary.Controls.Add(txtSummary);
		tabSummary.Location = new Point(4, 24);
		tabSummary.Name = "tabSummary";
		tabSummary.Padding = new Padding(3);
		tabSummary.Size = new Size(688, 599);
		tabSummary.TabIndex = 3;
		tabSummary.Text = "Summary";
		// 
		// txtSummary
		// 
		txtSummary.BackColor = Color.FromArgb(30, 30, 30);
		txtSummary.Dock = DockStyle.Fill;
		txtSummary.Font = new Font("Consolas", 10F);
		txtSummary.ForeColor = Color.FromArgb(212, 212, 212);
		txtSummary.Location = new Point(3, 3);
		txtSummary.Name = "txtSummary";
		txtSummary.ReadOnly = true;
		txtSummary.Size = new Size(682, 593);
		txtSummary.TabIndex = 0;
		txtSummary.Text = "";
		txtSummary.WordWrap = false;
		// 
		// statusStrip
		// 
		statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus, progressBar, lblFileCount });
		statusStrip.Location = new Point(0, 676);
		statusStrip.Name = "statusStrip";
		statusStrip.Size = new Size(1100, 24);
		statusStrip.TabIndex = 2;
		// 
		// lblStatus
		// 
		lblStatus.Name = "lblStatus";
		lblStatus.Size = new Size(1039, 19);
		lblStatus.Spring = true;
		lblStatus.Text = "Ready. Open RPX file(s) or folder to start.";
		lblStatus.TextAlign = ContentAlignment.MiddleLeft;
		// 
		// progressBar
		// 
		progressBar.Name = "progressBar";
		progressBar.Size = new Size(150, 18);
		progressBar.Visible = false;
		// 
		// lblFileCount
		// 
		lblFileCount.BorderSides = ToolStripStatusLabelBorderSides.Left;
		lblFileCount.Name = "lblFileCount";
		lblFileCount.Size = new Size(46, 19);
		lblFileCount.Text = "Files: 0";
		// 
		// MainForm
		// 
		AllowDrop = true;
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(1100, 700);
		Controls.Add(splitContainerMain);
		Controls.Add(toolStrip);
		Controls.Add(menuStrip);
		Controls.Add(statusStrip);
		MainMenuStrip = menuStrip;
		Name = "MainForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "RPX to C# Code Generator";
		menuStrip.ResumeLayout(false);
		menuStrip.PerformLayout();
		toolStrip.ResumeLayout(false);
		toolStrip.PerformLayout();
		splitContainerMain.Panel1.ResumeLayout(false);
		splitContainerMain.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
		splitContainerMain.ResumeLayout(false);
		tabControl.ResumeLayout(false);
		tabInitCode.ResumeLayout(false);
		tabControlsCode.ResumeLayout(false);
		tabControlsCode2.ResumeLayout(false);
		tabSummary.ResumeLayout(false);
		statusStrip.ResumeLayout(false);
		statusStrip.PerformLayout();
		ResumeLayout(false);
		PerformLayout();
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
	private ToolStripMenuItem refreshMenuItem;
	private ToolStripButton btnRefresh;

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
