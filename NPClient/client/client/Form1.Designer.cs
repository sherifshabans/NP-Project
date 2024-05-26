namespace client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            ChatArea = new TextBox();
            MessageArea = new TextBox();
            send = new Button();
            connect = new Button();
            directoryScreen = new TextBox();
            Directories = new Label();
            pictureBox1 = new PictureBox();
            sendOptionComboBox = new ComboBox();
            CloseButton = new Button();
            playvideo = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // ChatArea
            // 
            ChatArea.Location = new Point(12, 12);
            ChatArea.Multiline = true;
            ChatArea.Name = "ChatArea";
            ChatArea.Size = new Size(282, 329);
            ChatArea.TabIndex = 0;
            // 
            // MessageArea
            // 
            MessageArea.Location = new Point(12, 518);
            MessageArea.Multiline = true;
            MessageArea.Name = "MessageArea";
            MessageArea.Size = new Size(586, 56);
            MessageArea.TabIndex = 1;
            // 
            // send
            // 
            send.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            send.Location = new Point(12, 347);
            send.Name = "send";
            send.Size = new Size(123, 56);
            send.TabIndex = 2;
            send.Text = "send";
            send.UseVisualStyleBackColor = true;
            send.Click += send_Click;
            // 
            // connect
            // 
            connect.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            connect.Location = new Point(626, 518);
            connect.Name = "connect";
            connect.Size = new Size(123, 56);
            connect.TabIndex = 3;
            connect.Text = "connect";
            connect.UseVisualStyleBackColor = true;
            connect.Click += connect_Click;
            // 
            // directoryScreen
            // 
            directoryScreen.Location = new Point(626, 12);
            directoryScreen.Multiline = true;
            directoryScreen.Name = "directoryScreen";
            directoryScreen.Size = new Size(312, 329);
            directoryScreen.TabIndex = 4;
            // 
            // Directories
            // 
            Directories.AutoSize = true;
            Directories.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Directories.Location = new Point(626, 12);
            Directories.Name = "Directories";
            Directories.Size = new Size(131, 31);
            Directories.TabIndex = 5;
            Directories.Text = "Directories";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(300, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(320, 329);
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // sendOptionComboBox
            // 
            sendOptionComboBox.FormattingEnabled = true;
            sendOptionComboBox.Location = new Point(12, 409);
            sendOptionComboBox.Name = "sendOptionComboBox";
            sendOptionComboBox.Size = new Size(151, 28);
            sendOptionComboBox.TabIndex = 14;
            // 
            // CloseButton
            // 
            CloseButton.BackColor = Color.WhiteSmoke;
            CloseButton.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CloseButton.Location = new Point(787, 518);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(123, 56);
            CloseButton.TabIndex = 16;
            CloseButton.Text = "Close";
            CloseButton.UseVisualStyleBackColor = false;
            CloseButton.Click += button1_Click;
            // 
            // playvideo
            // 
            playvideo.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            playvideo.Location = new Point(345, 347);
            playvideo.Name = "playvideo";
            playvideo.Size = new Size(219, 90);
            playvideo.TabIndex = 17;
            playvideo.Text = "PlayVideo";
            playvideo.UseVisualStyleBackColor = true;
            playvideo.Click += playvideo_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1180, 586);
            Controls.Add(playvideo);
            Controls.Add(CloseButton);
            Controls.Add(sendOptionComboBox);
            Controls.Add(pictureBox1);
            Controls.Add(Directories);
            Controls.Add(directoryScreen);
            Controls.Add(connect);
            Controls.Add(send);
            Controls.Add(MessageArea);
            Controls.Add(ChatArea);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ChatArea;
        private TextBox MessageArea;
        private Button send;
        private Button connect;
        private TextBox directoryScreen;
        private Label Directories;
        private PictureBox pictureBox1;
        private ComboBox sendOptionComboBox;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private Button CloseButton;
        private Button playvideo;
    }
}
