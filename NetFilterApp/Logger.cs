using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace NetFilterApp
{
    class Logger
    {
        string logPath;
        FileStream logFileStream;
        UnicodeEncoding uniEncoding;

        public Logger(FileMode mode=FileMode.OpenOrCreate)
        {

            try
            {
                string directoryName = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                Type loggerType = typeof(Logger);
                string fileName = loggerType.Namespace;

                logPath = Path.ChangeExtension(Path.Combine(directoryName, fileName), "log");
                logFileStream = File.Open(logPath, mode, FileAccess.Write, FileShare.Read);

                uniEncoding = new UnicodeEncoding();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ~Logger()
        {
            logFileStream.Close();
        }

        public void write(string message)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();

                MethodBase method = stackTrace.GetFrame(1).GetMethod();

                string logLine = string.Format("{0} [{1}::{2}] {3}\n",
                    DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff"),
                    method.ReflectedType.Name, method.Name, message);

                long pos = logFileStream.Position;
                int byteCount = uniEncoding.GetByteCount(logLine);

                logFileStream.Lock(pos, byteCount);
                logFileStream.Write(uniEncoding.GetBytes(logLine), 0, byteCount);
                logFileStream.Unlock(pos, byteCount);

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
