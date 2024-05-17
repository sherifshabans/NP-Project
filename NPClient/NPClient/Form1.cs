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

namespace NPClient
{
    public partial class Form1 : Form
    {
        Socket server;
        NetworkStream ns;
        StreamReader sr;
        StreamWriter sw;
        private string selectedImagePath;

        public Form1()
        {
            InitializeComponent();
        }

        private  async void  ConnectButton_Click(object sender, EventArgs e)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6060);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            await Task.Run(() => server.Connect(ipep));
            ns = new NetworkStream(server);
            sw = new StreamWriter(ns);
            sr = new StreamReader(ns);


            // To make our client always be able to recieve an image.
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

                    // Array to recieve the size of the incoming image.
                    byte[] sizeData = new byte[4];

                    // Read the size asynchronously.
                    await ns.ReadAsync(sizeData, 0, sizeData.Length);

                    // Convert the size to integer form.
                    int imageSize = BitConverter.ToInt32(sizeData, 0);

                    // Array to recieve the image data.
                    byte[] imageData = new byte[imageSize];

                    // Variable to track the bytes read from the network stream.
                    int totalBytesRead = 0;

                    // Loop until all image data are read.
                    while (totalBytesRead < imageSize)
                    {
                        // Read image data asynchronously.
                        int bytesRead = await ns.ReadAsync(imageData, totalBytesRead, imageSize - totalBytesRead);

                        // Increment the total bytes read.
                        totalBytesRead += bytesRead;
                    }


                    string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
                    File.WriteAllBytes(savePath, imageData);

                    // Display the image in the PictureBox
                    using (MemoryStream memoryStream = new MemoryStream(imageData))
                    {
                  //      ImageShowBox.Image = Image.FromStream(memoryStream);
                    }

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
        }


        private void ChatArea_TextChanged(object sender, EventArgs e)
        {

        }

        private async void MessageArea_TextChanged(object sender, EventArgs e)
        {

            // chat message in send 
            string tmp = MessageArea.Text;
            sw.WriteLine(tmp);
            sw.Flush();
            MessageArea.Text = "";
            tmp = "Me (client): " + tmp;
            ChatArea.Text += tmp;
            ChatArea.AppendText(Environment.NewLine);

            while (true)
            {
                tmp = await Task.Run(() => sr.ReadLine());
                tmp = "Server: " + tmp;
                ChatArea.Text += tmp;
                ChatArea.AppendText(Environment.NewLine);
            }



        }

        private async void SendButton_Click(object sender, EventArgs e)
        {


            // for image 
            // Check if the path is null or empty.
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

                // Read all bytes from the path to a byte array.
                byte[] imageData = File.ReadAllBytes(selectedImagePath);

                // Convert the length of the image data to a byte array.
                // We do this to be able to send it on our network stream.
                byte[] sizeData = BitConverter.GetBytes(imageData.Length);

                // Send the size of the image.
                ns.Write(sizeData, 0, sizeData.Length);
                ns.Flush();

                // Send the image data.
                ns.Write(imageData, 0, imageData.Length);
                ns.Flush();

                // Just for debugging.
                // MessageBox.Show("Image sent successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // If an exception occurs during the sending process, show an error message box with the exception message.
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            // Create instance of the OpenFileDialog class.
          //  // https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-8.0
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the file filter to limit file selection to image files only
            openFileDialog.Filter = "All files (*.*)|*.*";
                 //            "All files (*.*)|*.*"

            // Set the initial filter index to the first option in the file filter
            openFileDialog.FilterIndex = 1;

            // Set a flag indicating whether the dialog box restores the current directory before closing
            // openFileDialog.RestoreDirectory = true;

            // Display the OpenFileDialog object and wait for the user to make a selection.
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // If the user selects a file and clicks OK,
                //      store the selected file path in the selectedImagePath variable.
                selectedImagePath = openFileDialog.FileName;

                // Set the PictureBox control's SizeMode property
                //      to stretch the image to fit the PictureBox dimensions.
               // ImageShowBox.SizeMode = PictureBoxSizeMode.StretchImage;

                // Show the selected image into the PictureBox.
              //  ImageShowBox.Image = new System.Drawing.Bitmap(openFileDialog.FileName, false);
            }
        }
    }
}
