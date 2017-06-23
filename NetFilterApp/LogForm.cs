using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NetFilterApp
{
    public partial class LogForm : Form
    {
        string logPath;

        public LogForm()
        {
            InitializeComponent();
        }

        public void OpenLogPath(string path)
        {
            if (File.Exists(path))
            {
                logPath = path;
                ReadFile();
                Show();
            }
            else
            {
                MessageBox.Show(string.Format("Log file {0} not found..", path), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogForm_Load(object sender, EventArgs e)
        {

        }

        private void reloadFileButton_Click(object sender, EventArgs e)
        {
            ReadFile();
        }

        private void ReadFile()
        {
            if (File.Exists(logPath))
            {
                try
                {
                    string tmpFile = Path.GetTempPath() + Guid.NewGuid().ToString();
                    File.Copy(logPath, tmpFile);

                    StreamReader sr = new StreamReader(tmpFile, true);

                    logTextBox.Text = "";
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        logTextBox.Text += line + "\r\n";
                    }

                    sr.Close();
                    File.Delete(tmpFile);
                }
                catch (Exception e)
                {
                    // write to log
                    MessageBox.Show(string.Format("{0} {1}", e.GetType().Name, e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            logTextBox.Text = "";
            Hide();
        }

        private void LogForm_Resize(object sender, EventArgs e)
        {
            logTextBox.Width = ClientSize.Width - 2 * logTextBox.Location.X;
            logTextBox.Height = ClientSize.Height - reloadFileButton.Height - 2 * logTextBox.Location.Y;

            reloadFileButton.Width = logTextBox.Width;
            reloadFileButton.Location = new Point(
                reloadFileButton.Location.X, logTextBox.Height + logTextBox.Location.Y);
        }
    }
}
