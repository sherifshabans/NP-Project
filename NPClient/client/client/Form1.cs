using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;

namespace client
{
    public partial class Form1 : Form
    {
        Socket server;
        NetworkStream ns;
        StreamReader sr;
        StreamWriter sw;

        public Form1()
        {
            InitializeComponent();
            sendOptionComboBox.Items.Add("Send Message");
            sendOptionComboBox.Items.Add("download directory: ");
            sendOptionComboBox.Items.Add("request file: ");
            sendOptionComboBox.Items.Add("request directory: ");
            sendOptionComboBox.SelectedIndex = 0; // Default to "Send Message"

        }

        private async void connect_Click(object sender, EventArgs e)
        {
            try
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6060);
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                connect.Enabled = false;

                await Task.Run(() => server.Connect(ipep));
                ns = new NetworkStream(server);
                sw = new StreamWriter(ns);
                sr = new StreamReader(ns, Encoding.UTF8);

                ReceiveMessages();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message);
                connect.Enabled = true;
            }
        }

        private async void ReceiveMessages()
        {
            try
            {
                string tmp;
                while ((tmp = await sr.ReadLineAsync()) != null)
                {
                    if (tmp.StartsWith("receive file"))
                    {
                        string message = tmp.Substring("receive file".Length).Trim();
                        // Extract the file type if it exists
                        string fileType = "";
                        if (message.StartsWith("(") && message.Contains(")"))
                        {
                            int startIndex = message.IndexOf("(") + 1;
                            int endIndex = message.IndexOf(")");
                            fileType = message.Substring(startIndex, endIndex - startIndex).ToLower().Trim();
                        }
                        ReceiveFile(fileType);
                    }
                    else if (tmp.StartsWith("receive directory"))
                    {
                        ReceiveDirectory();
                    }
                    else if (tmp.StartsWith("receive download directory"))
                    {
                        ReceiveDownloadDirectory();
                    }
                    else
                    {
                        tmp = "Server: " + tmp;
                        UpdateChat(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving message from server: " + ex.Message);
            }
        }

        private void UpdateChat(string message)
        {
            if (ChatArea.InvokeRequired)
            {
                ChatArea.Invoke((MethodInvoker)(() =>
                {
                    ChatArea.Text += message + Environment.NewLine;
                    ChatArea.ScrollToCaret();

                }));
            }
            else
            {
                ChatArea.Text += message + Environment.NewLine;
                ChatArea.ScrollToCaret();

            }
        }

        private void UpdateDirectoryScreen(string message)
        {
            if (directoryScreen.InvokeRequired)
            {
                directoryScreen.Invoke((MethodInvoker)(() =>
                {
                    directoryScreen.Text += message + Environment.NewLine;
                    directoryScreen.ScrollToCaret();

                }));
            }
            else
            {
                directoryScreen.Text += message + Environment.NewLine;
                directoryScreen.ScrollToCaret();

            }
        }

        private void SendMessage(string message)
        {
            try
            {
                if (sw != null)
                {
                    sw.WriteLine(message);
                    sw.Flush();
                }
                else
                {
                    MessageBox.Show("StreamWriter is not initialized.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending message to server: " + ex.Message);
            }
        }

        private void send_Click(object sender, EventArgs e)
        {
            string message = MessageArea.Text;
            if (!string.IsNullOrEmpty(message))
            {
                string temp = "";
                if (sendOptionComboBox.SelectedItem.ToString() == "request file: ")
                {
                    temp = "request file: ";

                    message = temp + message;
                    SendMessage(message);
                }
                else if (sendOptionComboBox.SelectedItem.ToString() == "download directory: ")
                {

                    temp = "download directory: ";

                    message = temp + message;
                    SendMessage(message);
                }
                else if (sendOptionComboBox.SelectedItem.ToString() == "request directory: ")
                {

                    temp = "request directory: ";

                    message = temp + message;
                    SendMessage(message);
                }
                else if (sendOptionComboBox.SelectedItem.ToString() == "Send Message")
                {

                    SendMessage(message);
                }


                message = "Me (client): " + message;
                UpdateChat(message);
                MessageArea.Text = "";
            }
        }
        //private byte[] SerializeFileTOBYTE(string filePath)
        //{
        //    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //    {
        //        using (BinaryReader br = new BinaryReader(fs))
        //        {
        //            byte[] fileBytes = br.ReadBytes((int)fs.Length);
        //            return fileBytes;
        //        }
        //    }

        //}


        //private void DeserializeByteToFile(byte[] fileBytes, string filePath)
        //{
        //    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        //    {
        //        using (BinaryWriter bw = new BinaryWriter(fs))
        //        {
        //            bw.Write(fileBytes);
        //        }
        //    }
        //}

        //private void SerializeFileTOJSON(string filePath)
        //{
        //    try
        //    {
        //        // Read the file contents as bytes
        //        byte[] fileBytes = File.ReadAllBytes(filePath);
        //        // Convert the bytes to base64 string for JSON serialization
        //        string base64String = Convert.ToBase64String(fileBytes);

        //        // Serialize the base64 string to JSON format
        //        string jsonData = JsonConvert.SerializeObject(base64String);

        //        // Write the JSON data to a file
        //        File.WriteAllText("serialized_file.json", jsonData);

        //        UpdateChat("File serialized: serialized_file.json");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error serializing the file: " + ex.Message);
        //    }
        //}

        //private void DeserializeFileJson(string jsonFilePath, string outputFilePath)
        //{
        //    try
        //    {
        //        string jsonData = File.ReadAllText(jsonFilePath);

        //        string base64String = JsonConvert.DeserializeObject<string>(jsonData);

        //        byte[] fileBytes = Convert.FromBase64String(base64String);

        //        File.WriteAllBytes(outputFilePath, fileBytes);

        //        UpdateChat("File deserialized: " + outputFilePath);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error deserializing the file: " + ex.Message);
        //    }
        //}

        private void ReceiveFile(string type)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead;
                string receivedFilePath = "received_file." + type;
                using (FileStream fs = new FileStream(receivedFilePath, FileMode.Create, FileAccess.Write))
                {
                    while ((bytesRead = ns.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, bytesRead);
                        if (ns.DataAvailable == false)
                        {
                            // No more data available, assume end of file
                            break;
                        }
                    }
                }

                if (type == "jpg" || type == "jpeg" || type == "png")
                {
                    using (FileStream imageStream = new FileStream(receivedFilePath, FileMode.Open, FileAccess.Read))
                    {
                        Image image = Image.FromStream(imageStream);
                        pictureBox1.Invoke((MethodInvoker)delegate
                        {
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            pictureBox1.Image = image;
                        });
                    }
                }

                //   byte[] fileBytes = SerializeFile(receivedFilePath);

                if (IsFileCompressed(receivedFilePath))
                {
                    string decompressedFilePath = "decompressed_file." + type;
                    DecompressFile(receivedFilePath, decompressedFilePath);
                    UpdateChat($"Compressed file received and decompressed to: {decompressedFilePath}");
                    MessageBox.Show("Compressed file received and decompressed");
                }
                else
                {
                    UpdateChat($"File received: {receivedFilePath}");
                    MessageBox.Show("File received");
                }
                // Clean up the compressed file after decompressing
                //if (File.Exists(compressedFilePath))
                //{
                //    File.Delete(compressedFilePath);
                //}

                // if the file is an image show the image in pictureBox1
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving the file: " + ex.Message);
            }
        }

        private bool IsFileCompressed(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] header = new byte[2];
                    fs.Read(header, 0, 2);
                    // Check for GZip header (0x1F, 0x8B)
                    return header[0] == 0x1F && header[1] == 0x8B;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void DecompressFile(string compressedFilePath, string decompressedFilePath)
        {
            using (FileStream compressedFileStream = new FileStream(compressedFilePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream decompressedFileStream = new FileStream(decompressedFilePath, FileMode.Create, FileAccess.Write))
                {
                    using (GZipStream decompressionStream = new GZipStream(compressedFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                }
            }
        }
        private void DecompressFileSeizlized(byte[] compressedFileBytes, string decompressedFilePath)
        {
            using (FileStream outputStream = new FileStream(decompressedFilePath, FileMode.Create, FileAccess.Write))
            {
                using (GZipStream decompressionStream = new GZipStream(new MemoryStream(compressedFileBytes), CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(outputStream);
                }
            }
        }

        private void ReceiveDirectory()
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead = ns.Read(buffer, 0, buffer.Length);
                string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                UpdateDirectoryScreen(receivedData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving directory information: " + ex.Message);
            }
        }
        private void ReceiveDownloadDirectory()
        {
            string targetPath = "C:\\Users\\sheri\\source\\repos\\Project\\GUI app\\client\\client\\bin\\Debug\\net8.0-windows";

            try
            {
                // Receive the number of files
                byte[] fileCountBytes = new byte[4];
                int receivedBytes = server.Receive(fileCountBytes);
                if (receivedBytes == 0)
                {
                    throw new Exception("No data received for file count.");
                }
                int fileCount = BitConverter.ToInt32(fileCountBytes, 0);

                for (int i = 0; i < fileCount; i++)
                {
                    // Receive relative path length
                    byte[] relativePathLengthBytes = new byte[4];
                    receivedBytes = server.Receive(relativePathLengthBytes);
                    if (receivedBytes == 0)
                    {
                        throw new Exception("No data received for relative path length.");
                    }
                    int relativePathLength = BitConverter.ToInt32(relativePathLengthBytes, 0);

                    // Receive relative path
                    byte[] relativePathBytes = new byte[relativePathLength];
                    receivedBytes = server.Receive(relativePathBytes);
                    if (receivedBytes == 0)
                    {
                        throw new Exception("No data received for relative path.");
                    }
                    string relativePath = Encoding.UTF8.GetString(relativePathBytes);

                    // Sanitize the relative path
                    relativePath = relativePath.Replace("/", "\\");
                    relativePath = relativePath.TrimEnd('\\');

                    // Ensure the relative path does not contain invalid characters
                    foreach (char invalidChar in Path.GetInvalidPathChars())
                    {
                        relativePath = relativePath.Replace(invalidChar, '_');
                    }

                    // Receive file size
                    byte[] fileSizeBytes = new byte[4];
                    receivedBytes = server.Receive(fileSizeBytes);
                    if (receivedBytes == 0)
                    {
                        throw new Exception("No data received for file size.");
                    }
                    int fileSize = BitConverter.ToInt32(fileSizeBytes, 0);

                    // Receive file content
                    byte[] fileBytes = new byte[fileSize];
                    int totalReceived = 0;
                    while (totalReceived < fileSize)
                    {
                        receivedBytes = server.Receive(fileBytes, totalReceived, fileSize - totalReceived, SocketFlags.None);
                        if (receivedBytes == 0)
                        {
                            throw new Exception("No data received for file content.");
                        }
                        totalReceived += receivedBytes;
                    }

                    // Recreate the directory structure
                    string fullPath = Path.Combine(targetPath, relativePath);
                    string directory = Path.GetDirectoryName(fullPath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Write the file content
                    File.WriteAllBytes(fullPath, fileBytes);
                }

                UpdateChat("Files successfully received: open this client\\client\\bin\\Debug\\net8.0-windows to see it");
                MessageBox.Show("Directory and files successfully received");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving directory files from server: " + ex.Message);
            }
        }


        private async void playStream_Click(object sender, EventArgs e)
        {

        }


        private bool IsEndOfFrame(byte[] buffer, int bytesRead)
        {
            // Check for end of frame marker or condition
            return bytesRead < buffer.Length;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }


        private async void axWindowsMediaPlayer1_Enter_1(object sender, EventArgs e)
        {


            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SendMessage("disconnect");

                if (sw != null) sw.Close();
                if (sr != null) sr.Close();
                if (ns != null) ns.Close();
                if (server != null) server.Close();

                connect.Enabled = true;

                UpdateChat("Disconnected from server.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error disconnecting from server: " + ex.Message);
            }

        }

        private async void playvideo_Click(object sender, EventArgs e)
        {
            try
            {
                // Connect to the server
                TcpClient client = new TcpClient();
                await client.ConnectAsync("127.0.0.1", 1234); // Connect to the server's IP address and port

                playvideo.Enabled = false;

                // Receive the video stream from the server
                NetworkStream ns = client.GetStream();
                byte[] buffer = new byte[4096]; // Buffer to hold incoming data

                // Continuously read data from the stream
                while (client.Connected)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int bytesRead;
                        while ((bytesRead = await ns.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, bytesRead);
                            if (IsEndOfFrame(buffer, bytesRead))
                            {
                                break;
                            }
                        }

                        // Convert the byte array to an image and display it
                        ms.Seek(0, SeekOrigin.Begin);
                        Image image = Image.FromStream(ms);
                        pictureBox1.Invoke((MethodInvoker)delegate
                        {
                            pictureBox1.Image = image;
                        });
                    }
                }

                // Close the connection with the server
                client.Close();
            }
            catch (IOException ex)
            {
                // Log the exception details
                Console.WriteLine("IOException while playing stream: " + ex.ToString());
            }
            catch (SocketException ex)
            {
                // Log the exception details
                Console.WriteLine("SocketException while playing stream: " + ex.ToString());
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine("Error playing stream: " + ex.ToString());
            }

        }
    }
}
