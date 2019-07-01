namespace WauzMusicPlayer
{
    partial class NewPlaylistForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewPlaylistForm));
            this.PlaylistNameInput = new System.Windows.Forms.TextBox();
            this.CreateButton = new System.Windows.Forms.Button();
            this.SelectedFilePathsBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // PlaylistNameInput
            // 
            this.PlaylistNameInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaylistNameInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlaylistNameInput.Location = new System.Drawing.Point(12, 12);
            this.PlaylistNameInput.Name = "PlaylistNameInput";
            this.PlaylistNameInput.Size = new System.Drawing.Size(359, 26);
            this.PlaylistNameInput.TabIndex = 0;
            this.PlaylistNameInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PlaylistNameInput_KeyDown);
            // 
            // CreateButton
            // 
            this.CreateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateButton.BackColor = System.Drawing.Color.Black;
            this.CreateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateButton.ForeColor = System.Drawing.Color.White;
            this.CreateButton.Location = new System.Drawing.Point(377, 13);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(75, 26);
            this.CreateButton.TabIndex = 1;
            this.CreateButton.Text = "CREATE";
            this.CreateButton.UseVisualStyleBackColor = false;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // SelectedFilePathsBox
            // 
            this.SelectedFilePathsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedFilePathsBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.SelectedFilePathsBox.ForeColor = System.Drawing.Color.White;
            this.SelectedFilePathsBox.Location = new System.Drawing.Point(12, 45);
            this.SelectedFilePathsBox.Multiline = true;
            this.SelectedFilePathsBox.Name = "SelectedFilePathsBox";
            this.SelectedFilePathsBox.ReadOnly = true;
            this.SelectedFilePathsBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SelectedFilePathsBox.Size = new System.Drawing.Size(440, 224);
            this.SelectedFilePathsBox.TabIndex = 2;
            this.SelectedFilePathsBox.WordWrap = false;
            // 
            // NewPlaylistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(464, 281);
            this.Controls.Add(this.SelectedFilePathsBox);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.PlaylistNameInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewPlaylistForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WauzMusicPlayer New Playlist";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PlaylistNameInput;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.TextBox SelectedFilePathsBox;
    }
}