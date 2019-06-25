namespace WauzMusicPlayer
{
    partial class NewPlaylistAdditionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewPlaylistAdditionForm));
            this.AddButton = new System.Windows.Forms.Button();
            this.SelectedFilePathsBox = new System.Windows.Forms.TextBox();
            this.PlaylistInput = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.BackColor = System.Drawing.Color.Black;
            this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddButton.ForeColor = System.Drawing.Color.White;
            this.AddButton.Location = new System.Drawing.Point(377, 13);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 26);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "ADD";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
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
            // PlaylistInput
            // 
            this.PlaylistInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaylistInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PlaylistInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlaylistInput.FormattingEnabled = true;
            this.PlaylistInput.Location = new System.Drawing.Point(12, 12);
            this.PlaylistInput.Name = "PlaylistInput";
            this.PlaylistInput.Size = new System.Drawing.Size(359, 26);
            this.PlaylistInput.TabIndex = 3;
            // 
            // NewPlaylistAdditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(464, 281);
            this.Controls.Add(this.PlaylistInput);
            this.Controls.Add(this.SelectedFilePathsBox);
            this.Controls.Add(this.AddButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewPlaylistAdditionForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WauzMusicPlayer Add To Playlist";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.TextBox SelectedFilePathsBox;
        private System.Windows.Forms.ComboBox PlaylistInput;
    }
}