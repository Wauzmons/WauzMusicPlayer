namespace WauzMusicPlayer
{
    partial class ConfigTagsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigTagsForm));
            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.YearTextBox = new System.Windows.Forms.TextBox();
            this.ArtistTextBox = new System.Windows.Forms.TextBox();
            this.AlbumTextBox = new System.Windows.Forms.TextBox();
            this.TrackTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TitleTextBox = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.NameLabel = new System.Windows.Forms.Label();
            this.TableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanel
            // 
            this.TableLayoutPanel.AutoSize = true;
            this.TableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TableLayoutPanel.ColumnCount = 2;
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableLayoutPanel.Controls.Add(this.YearTextBox, 1, 4);
            this.TableLayoutPanel.Controls.Add(this.ArtistTextBox, 1, 3);
            this.TableLayoutPanel.Controls.Add(this.AlbumTextBox, 1, 2);
            this.TableLayoutPanel.Controls.Add(this.TrackTextBox, 1, 1);
            this.TableLayoutPanel.Controls.Add(this.label5, 0, 4);
            this.TableLayoutPanel.Controls.Add(this.label4, 0, 3);
            this.TableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.TableLayoutPanel.Controls.Add(this.label2, 0, 1);
            this.TableLayoutPanel.Controls.Add(this.label3, 0, 2);
            this.TableLayoutPanel.Controls.Add(this.TitleTextBox, 1, 0);
            this.TableLayoutPanel.Location = new System.Drawing.Point(6, 33);
            this.TableLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.TableLayoutPanel.Name = "TableLayoutPanel";
            this.TableLayoutPanel.RowCount = 5;
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel.Size = new System.Drawing.Size(275, 125);
            this.TableLayoutPanel.TabIndex = 1;
            // 
            // YearTextBox
            // 
            this.YearTextBox.Location = new System.Drawing.Point(52, 103);
            this.YearTextBox.MaxLength = 4;
            this.YearTextBox.Name = "YearTextBox";
            this.YearTextBox.Size = new System.Drawing.Size(220, 20);
            this.YearTextBox.TabIndex = 5;
            this.YearTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.YearTextBox_KeyPress);
            // 
            // ArtistTextBox
            // 
            this.ArtistTextBox.Location = new System.Drawing.Point(52, 78);
            this.ArtistTextBox.MaxLength = 128;
            this.ArtistTextBox.Name = "ArtistTextBox";
            this.ArtistTextBox.Size = new System.Drawing.Size(220, 20);
            this.ArtistTextBox.TabIndex = 4;
            // 
            // AlbumTextBox
            // 
            this.AlbumTextBox.Location = new System.Drawing.Point(52, 53);
            this.AlbumTextBox.MaxLength = 128;
            this.AlbumTextBox.Name = "AlbumTextBox";
            this.AlbumTextBox.Size = new System.Drawing.Size(220, 20);
            this.AlbumTextBox.TabIndex = 3;
            // 
            // TrackTextBox
            // 
            this.TrackTextBox.Location = new System.Drawing.Point(52, 28);
            this.TrackTextBox.MaxLength = 4;
            this.TrackTextBox.Name = "TrackTextBox";
            this.TrackTextBox.Size = new System.Drawing.Size(220, 20);
            this.TrackTextBox.TabIndex = 2;
            this.TrackTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TrackTextBox_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Orange;
            this.label5.Location = new System.Drawing.Point(3, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Year";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Orange;
            this.label4.Location = new System.Drawing.Point(3, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "Artist";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Orange;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Title";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Orange;
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Track";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Orange;
            this.label3.Location = new System.Drawing.Point(3, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Album";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Location = new System.Drawing.Point(52, 3);
            this.TitleTextBox.MaxLength = 128;
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(220, 20);
            this.TitleTextBox.TabIndex = 1;
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.Black;
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.White;
            this.SaveButton.Location = new System.Drawing.Point(6, 164);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 26);
            this.SaveButton.TabIndex = 6;
            this.SaveButton.Text = "SAVE";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.Black;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.Location = new System.Drawing.Point(87, 164);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 26);
            this.CloseButton.TabIndex = 7;
            this.CloseButton.Text = "CANCEL";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.ForeColor = System.Drawing.Color.Orange;
            this.NameLabel.Location = new System.Drawing.Point(2, 9);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(263, 21);
            this.NameLabel.TabIndex = 5;
            this.NameLabel.Text = "This is the name of the Song.mp3";
            this.NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfigTagsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(322, 205);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.TableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigTagsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WauzMusicPlayer Tags";
            this.TableLayoutPanel.ResumeLayout(false);
            this.TableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TitleTextBox;
        private System.Windows.Forms.TextBox YearTextBox;
        private System.Windows.Forms.TextBox ArtistTextBox;
        private System.Windows.Forms.TextBox AlbumTextBox;
        private System.Windows.Forms.TextBox TrackTextBox;
        private System.Windows.Forms.Label NameLabel;
    }
}