using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NPServer
{

    public partial class Form1 : Form
    {

        Socket server;
        Socket client;
        NetworkStream ns;
        StreamReader sr;
        StreamWriter sw;


        // The path of our image.
        private string selectedImagePath;

        public Form1()
        {
            InitializeComponent();
        }

        private async void ChatArea_TextChanged(object sender, EventArgs e)
        {



        }

        private async void ListenButton_Click(object sender, EventArgs e)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6060);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipep);
            server.Listen(10);

            client = await Task.Run(() => server.Accept());
            ns = new NetworkStream(client);
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);

            // To make our server always be able to recieve an image.
            while (true)
            {
                try
                {

                    // Receive file name
                    byte[] fileNameLengthBuffer = new byte[4];
                    await ns.ReadAsync(fileNameLengthBuffer, 0, fileNameLengthBuffer.Length);
                    int fileNameLength = BitConverter.ToInt32(fileNameLengthBuffer, 0);

                    byte[] fileNameBuffer = new byte[fileNameLength];
                    await ns.ReadAsync(fileNameBuffer, 0, fileNameBuffer.Length);
                    string fileName = Encoding.UTF8.GetString(fileNameBuffer);

                    byte[] sizeData = new byte[4];
                    await ns.ReadAsync(sizeData, 0, sizeData.Length);
                    int imageSize = BitConverter.ToInt32(sizeData, 0);
                    byte[] imageData = new byte[imageSize];
                    int totalBytesRead = 0;

                    // Loop until all image data are read.
                    while (totalBytesRead < imageSize)
                    {
                        int bytesRead = await ns.ReadAsync(imageData, totalBytesRead, imageSize - totalBytesRead);
                        totalBytesRead += bytesRead;
                    }

                    string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
                    File.WriteAllBytes(savePath, imageData);

                    //  ImageShowBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    // Display the image in the PictureBox
                    using (MemoryStream memoryStream = new MemoryStream(imageData))
                    {
                    //    ImageShowBox.Image = Image.FromStream(memoryStream);
                    }

                    // Just for debugging.
                     MessageBox.Show("Image received and displayed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while receiving and displaying the image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            ns.Close();
            sw.Close();
            sr.Close();
            server.Close();
            client.Close();

        }

        private void MessageArea_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void SendButton_Click(object sender, EventArgs e)
        {

            // send image
            if (string.IsNullOrEmpty(selectedImagePath))
            {
                MessageBox.Show("Please select an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {

                // Send file name
                string fileName = Path.GetFileName(selectedImagePath);
                byte[] fileNameBuffer = Encoding.UTF8.GetBytes(fileName);
                byte[] fileNameLengthBuffer = BitConverter.GetBytes(fileNameBuffer.Length);
                await ns.WriteAsync(fileNameLengthBuffer, 0, fileNameLengthBuffer.Length);
                await ns.WriteAsync(fileNameBuffer, 0, fileNameBuffer.Length);


                byte[] imageData = File.ReadAllBytes(selectedImagePath);
                byte[] sizeData = BitConverter.GetBytes(imageData.Length);

                ns.Write(sizeData, 0, sizeData.Length);
                ns.Flush();

                ns.Write(imageData, 0, imageData.Length);
                ns.Flush();

                // For debugging.
                // MessageBox.Show("Image sent successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            // send message 

            string tmp = MessageArea.Text;
            sw.WriteLine(tmp);
            sw.Flush();
            MessageArea.Text = "";
            tmp = "Me (server): " + tmp;
            ChatArea.Text += tmp;
            ChatArea.AppendText(Environment.NewLine);

            while (true)
            {
                tmp = await Task.Run(() => sr.ReadLine());
                tmp = "Client: " + tmp;
                ChatArea.Text += tmp;
                ChatArea.AppendText(Environment.NewLine);

            }



        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = openFileDialog.FileName;
               // ImageShowBox.SizeMode = PictureBoxSizeMode.StretchImage;
             //   ImageShowBox.Image = new System.Drawing.Bitmap(openFileDialog.FileName, false);
            }

        }
    }
    
}
