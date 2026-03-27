using GrapeCity.ActiveReports.Viewer.Win;
using System.Text.Json;

namespace KKReport
{
	partial class ViewerForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}

			base.Dispose(disposing);
		}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openFileDialog = new OpenFileDialog();
			viewer = new Viewer();

			SuspendLayout();
            // 
            // viewer
            // 
            viewer.CurrentPage = 0;
            viewer.Dock = DockStyle.Fill;
            viewer.Font = new Font("Segoe UI", 9F);
            viewer.Location = new Point(0, 0);
            viewer.Name = "viewer";
            viewer.PagesBackColor = Color.FromArgb(128, 128, 128, 128);
            viewer.PreviewPages = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            viewer.Sidebar.ParametersPanel.ContextMenu = null;
            viewer.Sidebar.ParametersPanel.Text = "Parameters";
            viewer.Sidebar.ParametersPanel.Width = 200;
            // 
            // 
            // 
            viewer.Sidebar.SearchPanel.ContextMenu = null;
            viewer.Sidebar.SearchPanel.Text = "Search results";
            viewer.Sidebar.SearchPanel.Width = 200;
            // 
            // 
            // 
            viewer.Sidebar.ThumbnailsPanel.ContextMenu = null;
            viewer.Sidebar.ThumbnailsPanel.Text = "Page thumbnails";
            viewer.Sidebar.ThumbnailsPanel.Width = 200;
            viewer.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
            // 
            // 
            // 
            viewer.Sidebar.TocPanel.ContextMenu = null;
            viewer.Sidebar.TocPanel.Expanded = true;
            viewer.Sidebar.TocPanel.Text = "Document map";
            viewer.Sidebar.TocPanel.Width = 200;
            viewer.Sidebar.Width = 200;
            viewer.Size = new Size(626, 527);
            viewer.TabIndex = 0;
			viewer.UseHyperlinkSettings = true;
			viewer.HyperlinkForeColor = Color.Black;
			viewer.HyperlinkUnderline = false;
			viewer.HyperlinkBackColor = Color.Transparent;
			viewer.HyperLink += Viewer_HyperLink;

			// 
			// ViewerForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(626, 527);
            Controls.Add(viewer);
            Name = "ViewerForm";
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private Viewer viewer;
    }
}
