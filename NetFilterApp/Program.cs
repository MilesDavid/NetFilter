using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace NetFilterApp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
        static bool SameProceessRunned()
        {
            var currentProcess = Process.GetCurrentProcess();

            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    if (process.MainModule.FileName == currentProcess.MainModule.FileName &&
                        process.Id != currentProcess.Id)
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
                            return true;
                        }
                    }
                }
                catch (Exception e) { } 
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
