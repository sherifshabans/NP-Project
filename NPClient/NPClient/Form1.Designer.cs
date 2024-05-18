namespace NPClient
{
    partial class Form1
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
            this.ChatArea = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.MessageArea = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.ImageShowBox = new System.Windows.Forms.PictureBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.sendOptionComboBox = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.ImageShowBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ChatArea
            // 
            this.ChatArea.Location = new System.Drawing.Point(12, 12);
            this.ChatArea.Multiline = true;
            this.ChatArea.Name = "ChatArea";
            this.ChatArea.Size = new System.Drawing.Size(310, 80);
            this.ChatArea.TabIndex = 4;
            this.ChatArea.TextChanged += new System.EventHandler(this.ChatArea_TextChanged);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectButton.Location = new System.Drawing.Point(769, 12);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(567, 64);
            this.ConnectButton.TabIndex = 5;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(769, 503);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(567, 75);
            this.CloseButton.TabIndex = 6;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // MessageArea
            // 
            this.MessageArea.Location = new System.Drawing.Point(12, 113);
            this.MessageArea.Multiline = true;
            this.MessageArea.Name = "MessageArea";
            this.MessageArea.Size = new System.Drawing.Size(310, 63);
            this.MessageArea.TabIndex = 7;
            this.MessageArea.TextChanged += new System.EventHandler(this.MessageArea_TextChanged);
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(769, 98);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(567, 54);
            this.BrowseButton.TabIndex = 8;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // ImageShowBox
            // 
            this.ImageShowBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ImageShowBox.Location = new System.Drawing.Point(12, 193);
            this.ImageShowBox.Name = "ImageShowBox";
            this.ImageShowBox.Size = new System.Drawing.Size(310, 217);
            this.ImageShowBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImageShowBox.TabIndex = 9;
            this.ImageShowBox.TabStop = false;
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(769, 170);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(567, 55);
            this.SendButton.TabIndex = 10;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // sendOptionComboBox
            // 
            this.sendOptionComboBox.FormattingEnabled = true;
            this.sendOptionComboBox.Location = new System.Drawing.Point(901, 257);
            this.sendOptionComboBox.Name = "sendOptionComboBox";
            this.sendOptionComboBox.Size = new System.Drawing.Size(121, 24);
            this.sendOptionComboBox.TabIndex = 11;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(441, 79);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(203, 251);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 590);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.sendOptionComboBox);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ImageShowBox);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.MessageArea);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.ChatArea);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ImageShowBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ChatArea;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.TextBox MessageArea;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.PictureBox ImageShowBox;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.ComboBox sendOptionComboBox;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

