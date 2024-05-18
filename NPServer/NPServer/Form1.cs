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
        private string selectedPath;
        private string selectedDirectoryPath;


        public Form1()
        {
            InitializeComponent();
            sendOptionComboBox.Items.Add("Send Message");
            sendOptionComboBox.Items.Add("Send File");
            sendOptionComboBox.Items.Add("Send Directory");
            sendOptionComboBox.SelectedIndex = 0; // Default to "Send Message"

        }


        private void ChatArea_TextChanged(object sender, EventArgs e)
        {

        }
        private async void MessageArea_TextChanged(object sender, EventArgs e)
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
                    // Receive command
                    string command = await sr.ReadLineAsync();
                 //   MessageBox.Show($"{command}this is the command");

                    if (command == "DIRECTORY")
                    {
                        // Receive directory name
                        byte[] dirNameLengthBuffer = new byte[4];
                        await ns.ReadAsync(dirNameLengthBuffer, 0, dirNameLengthBuffer.Length);
                        int dirNameLength = BitConverter.ToInt32(dirNameLengthBuffer, 0);

                        byte[] dirNameBuffer = new byte[dirNameLength];
                        await ns.ReadAsync(dirNameBuffer, 0, dirNameBuffer.Length);
                        string dirName = Encoding.UTF8.GetString(dirNameBuffer);

                        // Receive the number of files
                        byte[] fileCountBuffer = new byte[4];
                        await ns.ReadAsync(fileCountBuffer, 0, fileCountBuffer.Length);
                        int fileCount = BitConverter.ToInt32(fileCountBuffer, 0);

                        // Create directory
                        string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dirName);
                        Directory.CreateDirectory(dirPath);

                        // Receive each file
                        for (int i = 0; i < fileCount; i++)
                        {
                            // Receive file name
                            byte[] fileNameLengthBuffer = new byte[4];
                            await ns.ReadAsync(fileNameLengthBuffer, 0, fileNameLengthBuffer.Length);
                            int fileNameLength = BitConverter.ToInt32(fileNameLengthBuffer, 0);

                            byte[] fileNameBuffer = new byte[fileNameLength];
                            await ns.ReadAsync(fileNameBuffer, 0, fileNameBuffer.Length);
                            string fileName = Encoding.UTF8.GetString(fileNameBuffer);

                            // Receive file size
                            byte[] sizeData = new byte[4];
                            await ns.ReadAsync(sizeData, 0, sizeData.Length);
                            int fileSize = BitConverter.ToInt32(sizeData, 0);

                            // Receive file data
                            byte[] fileData = new byte[fileSize];
                            int totalBytesRead = 0;

                            while (totalBytesRead < fileSize)
                            {
                                int bytesRead = await ns.ReadAsync(fileData, totalBytesRead, fileSize - totalBytesRead);
                                totalBytesRead += bytesRead;
                            }

                            // Save file
                            string filePath = Path.Combine(dirPath, fileName);
                            File.WriteAllBytes(filePath, fileData);
                        }

                        MessageBox.Show($"Directory {dirName} received and saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DisplayDirectoryFiles(dirPath);
                    }

                    else if (command == "FILE")
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

                        //string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
                        //File.WriteAllBytes(savePath, imageData);

                        string savePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
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
                    else
                    {
                        string tmp = "";
                        while (true)
                        {
                            tmp = await sr.ReadLineAsync();
                            if (string.IsNullOrEmpty(tmp))
                            {
                                break; // Exit the loop when there are no more lines to read
                            }
                            tmp = "Client: " + tmp;
                            Invoke(new Action(() =>
                            {
                                ChatArea.Text += tmp;
                                ChatArea.AppendText(Environment.NewLine);
                            }));
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while receiving and displaying the image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }
        }
        private void DisplayDirectoryFiles(string dirPath)
        {
            // Clear existing items
            listView1.Items.Clear();

            // Get files in the directory
            string[] files = Directory.GetFiles(dirPath);

            // Add files to the list view
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                listView1.Items.Add(fileName);
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


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void SendButton_Click(object sender, EventArgs e)
        {



            if (sendOptionComboBox.SelectedItem.ToString() == "Send Message")
            {
                SendMessage();
            }
            else if (sendOptionComboBox.SelectedItem.ToString() == "Send File")
            {
                SendFile();
            }
            else if (sendOptionComboBox.SelectedItem.ToString() == "Send Directory")
            {
                SendDirectory();
            }


        }
        private  async void SendMessage()
        {


            // send message 
            string tmp = MessageArea.Text;
            sw.WriteLine(tmp);
            sw.Flush();
            MessageArea.Text = "";
            if (string.IsNullOrEmpty(tmp))
            {


                tmp = "Me (server): " + tmp;
                ChatArea.Text += tmp;
                ChatArea.AppendText(Environment.NewLine);

            }



        }

        private async void SendFile()
        {


            sw.WriteLine("FILE");
            sw.Flush();

            // send image
            if (string.IsNullOrEmpty(selectedPath))
            {
                MessageBox.Show("Please select an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {

                // Send file name
                string fileName = Path.GetFileName(selectedPath);
                byte[] fileNameBuffer = Encoding.UTF8.GetBytes(fileName);
                byte[] fileNameLengthBuffer = BitConverter.GetBytes(fileNameBuffer.Length);
                await ns.WriteAsync(fileNameLengthBuffer, 0, fileNameLengthBuffer.Length);
                await ns.WriteAsync(fileNameBuffer, 0, fileNameBuffer.Length);


                byte[] imageData = File.ReadAllBytes(selectedPath);
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


        }
        private async void SendDirectory()
        {
            selectedDirectoryPath = "G:\\Desktop\\CV";

            if (string.IsNullOrEmpty(selectedDirectoryPath))
            {
                MessageBox.Show("Please select a directory first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                sw.WriteLine("DIRECTORY");
                sw.Flush();

                // Send directory name
                string dirName = new DirectoryInfo(selectedDirectoryPath).Name;
                byte[] dirNameBuffer = Encoding.UTF8.GetBytes(dirName);
                byte[] dirNameLengthBuffer = BitConverter.GetBytes(dirNameBuffer.Length);
                await ns.WriteAsync(dirNameLengthBuffer, 0, dirNameLengthBuffer.Length);
                await ns.WriteAsync(dirNameBuffer, 0, dirNameBuffer.Length);

                // Get all files in the directory
                string[] files = Directory.GetFiles(selectedDirectoryPath);

                // Send the number of files
                byte[] fileCountBuffer = BitConverter.GetBytes(files.Length);
                await ns.WriteAsync(fileCountBuffer, 0, fileCountBuffer.Length);

                // Send each file
                foreach (string filePath in files)
                {
                    // Send file name
                    string fileName = Path.GetFileName(filePath);
                    byte[] fileNameBuffer = Encoding.UTF8.GetBytes(fileName);
                    byte[] fileNameLengthBuffer = BitConverter.GetBytes(fileNameBuffer.Length);
                    await ns.WriteAsync(fileNameLengthBuffer, 0, fileNameLengthBuffer.Length);
                    await ns.WriteAsync(fileNameBuffer, 0, fileNameBuffer.Length);

                    // Read all bytes from the path to a byte array.
                    byte[] fileData = File.ReadAllBytes(filePath);

                    // Convert the length of the file data to a byte array.
                    byte[] sizeData = BitConverter.GetBytes(fileData.Length);

                    // Send the size of the file.
                    ns.Write(sizeData, 0, sizeData.Length);
                    ns.Flush();

                    // Send the file data.
                    ns.Write(fileData, 0, fileData.Length);
                    ns.Flush();
                }

                MessageBox.Show("Directory sent successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                selectedPath = openFileDialog.FileName;
               // ImageShowBox.SizeMode = PictureBoxSizeMode.StretchImage;
             //   ImageShowBox.Image = new System.Drawing.Bitmap(openFileDialog.FileName, false);
            }

        }
    }
    
}
