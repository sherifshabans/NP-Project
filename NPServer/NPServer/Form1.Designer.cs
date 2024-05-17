﻿namespace NPServer
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
            this.MessageArea = new System.Windows.Forms.TextBox();
            this.ListenButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.ImageShowBox = new System.Windows.Forms.PictureBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImageShowBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ChatArea
            // 
            this.ChatArea.Location = new System.Drawing.Point(28, 23);
            this.ChatArea.Multiline = true;
            this.ChatArea.Name = "ChatArea";
            this.ChatArea.Size = new System.Drawing.Size(314, 52);
            this.ChatArea.TabIndex = 9;
            this.ChatArea.TextChanged += new System.EventHandler(this.ChatArea_TextChanged);
            // 
            // MessageArea
            // 
            this.MessageArea.Location = new System.Drawing.Point(28, 102);
            this.MessageArea.Multiline = true;
            this.MessageArea.Name = "MessageArea";
            this.MessageArea.Size = new System.Drawing.Size(314, 63);
            this.MessageArea.TabIndex = 10;
            this.MessageArea.TextChanged += new System.EventHandler(this.MessageArea_TextChanged);
            // 
            // ListenButton
            // 
            this.ListenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListenButton.Location = new System.Drawing.Point(866, 23);
            this.ListenButton.Name = "ListenButton";
            this.ListenButton.Size = new System.Drawing.Size(455, 72);
            this.ListenButton.TabIndex = 11;
            this.ListenButton.Text = "Listen";
            this.ListenButton.UseVisualStyleBackColor = true;
            this.ListenButton.Click += new System.EventHandler(this.ListenButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(866, 481);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(455, 67);
            this.CloseButton.TabIndex = 12;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ImageShowBox
            // 
            this.ImageShowBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ImageShowBox.Location = new System.Drawing.Point(28, 204);
            this.ImageShowBox.Name = "ImageShowBox";
            this.ImageShowBox.Size = new System.Drawing.Size(314, 254);
            this.ImageShowBox.TabIndex = 13;
            this.ImageShowBox.TabStop = false;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(866, 120);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(455, 63);
            this.BrowseButton.TabIndex = 14;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(866, 204);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(455, 64);
            this.SendButton.TabIndex = 15;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 560);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.ImageShowBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ListenButton);
            this.Controls.Add(this.MessageArea);
            this.Controls.Add(this.ChatArea);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImageShowBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ChatArea;
        private System.Windows.Forms.TextBox MessageArea;
        private System.Windows.Forms.Button ListenButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.PictureBox ImageShowBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Button SendButton;
    }
}
