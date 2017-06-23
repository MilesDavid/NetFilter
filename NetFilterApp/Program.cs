using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace NetFilterApp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
        static bool SameProceessRunned(bool checkHashSum=false)
        {
            var currentProcess = Process.GetCurrentProcess();

            List<string> errors = new List<string>();
            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    if (process.ProcessName == currentProcess.ProcessName && process.Id != currentProcess.Id)
                    {
                        if (checkHashSum)
                        {
                            byte[] processHash, currentProcessHash;
                            using (var md5 = MD5.Create())
                            {
                                using (var streamProcess = File.OpenRead(process.MainModule.FileName))
                                    processHash = md5.ComputeHash(streamProcess);

                                using (var streamCurrentProcess = File.OpenRead(currentProcess.MainModule.FileName))
                                    currentProcessHash = md5.ComputeHash(streamCurrentProcess);
                            }

                            if (processHash.SequenceEqual(currentProcessHash))
                            {
                                if (errors.Count > 0)
                                {
                                    MessageBox.Show(string.Join("\r\n", errors));
                                }

                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                catch (Exception e) {
                    errors.Add(string.Format("[{0}] {1} {2} stack: {3}", process.ProcessName, e.GetType(), e.Message, e.StackTrace));
                }
            }

            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\r\n", errors));
            }

            return false;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!SameProceessRunned())
            {
                Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show("Netfilter already started", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
