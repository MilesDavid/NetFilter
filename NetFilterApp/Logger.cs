using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace NetFilterApp
{
    class Logger : IDisposable
    {
        string logPath;
        StreamWriter logFileStream;

        public Logger(FileMode mode=FileMode.Create)
        {
            try
            {
                string directoryName = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                Type loggerType = typeof(Logger);
                string fileName = loggerType.Namespace;

                logPath = Path.ChangeExtension(Path.Combine(directoryName, fileName), "log");
                logFileStream = new StreamWriter(
                    File.Open(logPath, mode, FileAccess.Write, FileShare.Read), Encoding.UTF8);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                logFileStream.Close();
            }
        }

        ~Logger()
        {
            Dispose(false);
        }

        public string LogPath
        {
            get
            {
                return logPath;
            }
        }

        public void write(string message)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();

                MethodBase method = stackTrace.GetFrame(1).GetMethod();

                string logLine = string.Format("{0} [{1}::{2}] {3}",
                    DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff"),
                    method.ReflectedType.Name, method.Name, message);

                logFileStream.WriteLine(logLine);
                logFileStream.Flush();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
