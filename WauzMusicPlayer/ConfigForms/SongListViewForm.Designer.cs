namespace WauzMusicPlayer
{
    partial class SongListViewForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SongListViewForm));
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.SongViewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToQueueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAllToQueueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showTagEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSonglistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeFromPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlaylistMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deletePlaylistToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.SongViewMenu.SuspendLayout();
            this.PlaylistMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridView
            // 
            this.DataGridView.AllowUserToAddRows = false;
            this.DataGridView.AllowUserToDeleteRows = false;
            this.DataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(25)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.DataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.NullValue = "???";
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(25)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView.Location = new System.Drawing.Point(0, 0);
            this.DataGridView.MultiSelect = false;
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.ReadOnly = true;
            this.DataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(25)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.DataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView.Size = new System.Drawing.Size(800, 450);
            this.DataGridView.TabIndex = 0;
            // 
            // SongViewMenu
            // 
            this.SongViewMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.addToQueueToolStripMenuItem,
            this.addAllToQueueToolStripMenuItem,
            this.toolStripSeparator2,
            this.showTagEditorToolStripMenuItem,
            this.showSonglistToolStripMenuItem,
            this.toolStripSeparator1,
            this.removeFromPlaylistToolStripMenuItem,
            this.deletePlaylistToolStripMenuItem});
            this.SongViewMenu.Name = "SongViewMenu";
            this.SongViewMenu.Size = new System.Drawing.Size(209, 170);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.playToolStripMenuItem.Text = "Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.PlayToolStripMenuItem_Click);
            // 
            // addToQueueToolStripMenuItem
            // 
            this.addToQueueToolStripMenuItem.Name = "addToQueueToolStripMenuItem";
            this.addToQueueToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.addToQueueToolStripMenuItem.Text = "Add To Queue";
            this.addToQueueToolStripMenuItem.Click += new System.EventHandler(this.AddToQueueToolStripMenuItem_Click);
            // 
            // addAllToQueueToolStripMenuItem
            // 
            this.addAllToQueueToolStripMenuItem.Name = "addAllToQueueToolStripMenuItem";
            this.addAllToQueueToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.addAllToQueueToolStripMenuItem.Text = "Add All To Queue";
            this.addAllToQueueToolStripMenuItem.Click += new System.EventHandler(this.AddAllToQueueToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
            // 
            // showTagEditorToolStripMenuItem
            // 
            this.showTagEditorToolStripMenuItem.Name = "showTagEditorToolStripMenuItem";
            this.showTagEditorToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.showTagEditorToolStripMenuItem.Text = "Show Tag Editor";
            this.showTagEditorToolStripMenuItem.Click += new System.EventHandler(this.ShowTagEditorToolStripMenuItem_Click);
            // 
            // showSonglistToolStripMenuItem
            // 
            this.showSonglistToolStripMenuItem.Name = "showSonglistToolStripMenuItem";
            this.showSonglistToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.showSonglistToolStripMenuItem.Text = "Show Songlist in Window";
            this.showSonglistToolStripMenuItem.Click += new System.EventHandler(this.ShowSonglistToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
            // 
            // removeFromPlaylistToolStripMenuItem
            // 
            this.removeFromPlaylistToolStripMenuItem.Name = "removeFromPlaylistToolStripMenuItem";
            this.removeFromPlaylistToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.removeFromPlaylistToolStripMenuItem.Text = "Remove From Playlist";
            this.removeFromPlaylistToolStripMenuItem.Click += new System.EventHandler(this.RemoveFromPlaylistToolStripMenuItem_Click);
            // 
            // deletePlaylistToolStripMenuItem
            // 
            this.deletePlaylistToolStripMenuItem.Name = "deletePlaylistToolStripMenuItem";
            this.deletePlaylistToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.deletePlaylistToolStripMenuItem.Text = "Delete Playlist";
            this.deletePlaylistToolStripMenuItem.Click += new System.EventHandler(this.DeletePlaylistToolStripMenuItem_Click);
            // 
            // PlaylistMenu
            // 
            this.PlaylistMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deletePlaylistToolStripMenuItem1});
            this.PlaylistMenu.Name = "PlaylistMenuStrip";
            this.PlaylistMenu.Size = new System.Drawing.Size(148, 26);
            // 
            // deletePlaylistToolStripMenuItem1
            // 
            this.deletePlaylistToolStripMenuItem1.Name = "deletePlaylistToolStripMenuItem1";
            this.deletePlaylistToolStripMenuItem1.Size = new System.Drawing.Size(147, 22);
            this.deletePlaylistToolStripMenuItem1.Text = "Delete Playlist";
            this.deletePlaylistToolStripMenuItem1.Click += new System.EventHandler(this.DeletePlaylistToolStripMenuItem_Click);
            // 
            // SongListViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(25)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SongListViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WauzMusicPlayer Song List";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.SongViewMenu.ResumeLayout(false);
            this.PlaylistMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridView;
        private System.Windows.Forms.ContextMenuStrip SongViewMenu;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToQueueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAllToQueueToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem removeFromPlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePlaylistToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip PlaylistMenu;
        private System.Windows.Forms.ToolStripMenuItem deletePlaylistToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem showSonglistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTagEditorToolStripMenuItem;
    }
}