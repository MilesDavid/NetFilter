using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NetfilterInstaller
{
    static class DriverInstaller
    {
        const string driverName = "netfilter2.sys";

        public enum DriverType
        {
            TDI = 100,
            WFP = 200
        }

        enum RegAction
        {
            Register = 300,
            Unregister = 400
        }

        #region kernel32.dll methods
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);
        #endregion

        static void SetUninstallKeyStarted()
        {
            RegistryKey netfilterRegKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Netfilter\\Uninstall", true);
            netfilterRegKey.SetValue("UnregDriver", "1", RegistryValueKind.DWord);
        }

        static bool UninstallStarted()
        {
            RegistryKey netfilterRegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Netfilter\\Uninstall", true);
            if (netfilterRegKey != null)
            {
                var unregDriverValue = netfilterRegKey.GetValue("UnregDriver");

                return true;
            }
            else
            {
                return false;
            }
        }

        static void RunRegUtil(RegAction act)
        {
            Process pProcess = new Process();
            pProcess.StartInfo.FileName = "nfregdrv.exe";

            if (act == RegAction.Register)
            {
                pProcess.StartInfo.Arguments = Path.GetFileNameWithoutExtension(driverName);
            }
            else
            {
                pProcess.StartInfo.Arguments = string.Format("-u {0}",
                    Path.GetFileNameWithoutExtension(driverName));
            }

            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            pProcess.Start();
            pProcess.WaitForExit();
        }

        static public bool InstallDriver(ref string errorMsg, DriverType driverType)
        {
            Dictionary<string, string> srcDriverPathDict = new Dictionary<string, string>();

            string dstDriverPath = Environment.ExpandEnvironmentVariables("%windir%\\system32\\drivers");

            srcDriverPathDict.Add("driverDir", Path.Combine(
                Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "drivers"));

            string srcDriverPath = string.Empty;

            if (driverType == DriverType.WFP)
            {
                srcDriverPathDict.Add("driverVersion", "wfp");
                string osVersion = OsProxy.GetOsVersion();
                if (osVersion == string.Empty)
                {
                    errorMsg = "Couldn't get OS version";
                    return false;
                }

                srcDriverPathDict.Add("osVersion", osVersion);
            }
            else
            {
                srcDriverPathDict.Add("driverVersion", "tdi");
            }

            srcDriverPathDict.Add("bitCapasity", (!Environment.Is64BitOperatingSystem) ? "i386" : "amd64");
            srcDriverPathDict.Add("driverName", driverName);

            string[] srcDriverPathArr = new string[srcDriverPathDict.Count];
            srcDriverPathDict.Values.CopyTo(srcDriverPathArr, 0);

            srcDriverPath = Path.Combine(srcDriverPathArr);
            dstDriverPath = Path.Combine(dstDriverPath, srcDriverPathDict["driverName"]);

            try
            {
                IntPtr oldValue = IntPtr.Zero;
                if (Wow64DisableWow64FsRedirection(ref oldValue))
                {
                    File.Copy(srcDriverPath, dstDriverPath);
                    if (Wow64RevertWow64FsRedirection(oldValue) == false)
                    {
                        errorMsg = "Couldn't revert Wow64 redirection";
                        return false;
                    }
                }
                else
                {
                    errorMsg = "Couldn't disable Wow64 redirection";
                    return false;
                }
            }
            catch (FileNotFoundException)
            {
                errorMsg = "driver not found";
                return false;
            }
            catch (IOException)
            {
                errorMsg = "driver already exists";
                return false;
            }
            catch (Exception e)
            {
                errorMsg = string.Format("exception {0} {1}", e.GetType(), e.Message);
                return false;
            }

            //Register driver here..
            RunRegUtil(RegAction.Register);

            return true;
        }

        static public bool DriverAlreadyExits(ref string errorMsg)
        {
            string driverPath = Path.Combine(Environment.ExpandEnvironmentVariables(
                "%windir%\\system32\\drivers"), driverName);

            bool result = false;

            IntPtr oldValue = IntPtr.Zero;
            if (Wow64DisableWow64FsRedirection(ref oldValue))
            {
                result = File.Exists(driverPath);
                if (Wow64RevertWow64FsRedirection(oldValue) == false)
                {
                    errorMsg = "Couldn't revert Wow64 redirection";
                    return false;
                }
            }
            else
            {
                errorMsg = "Couldn't disable Wow64 redirection";
                return false;
            }

            return result;
        }

        static public bool DeleteDriver(ref string errorMsg)
        {
            string driverPath = Path.Combine(Environment.ExpandEnvironmentVariables(
                "%windir%\\system32\\drivers"), driverName);

            IntPtr oldValue = IntPtr.Zero;
            try
            {
                // Unregistrate here..
                if (!UninstallStarted())
                {
                    RunRegUtil(RegAction.Unregister);
                    SetUninstallKeyStarted();
                }
                else
                {
                    if (Wow64DisableWow64FsRedirection(ref oldValue))
                    {
                        File.Delete(driverPath);

                        if (Wow64RevertWow64FsRedirection(oldValue) == false)
                        {
                            errorMsg = "Couldn't revert Wow64 redirection";
                            return false;
                        }
                    }
                    else
                    {
                        errorMsg = "Couldn't disable Wow64 redirection";
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                errorMsg = string.Format("Error: {0} {1}", e.GetType(), e.Message);
                return false;
            }

            return !DriverAlreadyExits(ref errorMsg);
        }
    }
}
