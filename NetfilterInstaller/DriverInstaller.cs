using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
                File.Copy(srcDriverPath, dstDriverPath);
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

            return true;
        }

        static public bool DriverAlreadyExits()
        {
            string driverPath = Path.Combine(Environment.ExpandEnvironmentVariables(
                "%windir%\\system32\\drivers"), driverName);
            return File.Exists(driverPath);
        }

        static public bool DeleteDriver(ref string errorMsg)
        {
            string driverPath = Path.Combine(Environment.ExpandEnvironmentVariables(
                "%windir%\\system32\\drivers"), driverName);
            try
            {
                File.Delete(driverPath);
            }
            catch (Exception e)
            {
                errorMsg = string.Format("Error: {0} {1}", e.GetType(), e.Message);
                return false;
            }

            return !DriverAlreadyExits();
        }
    }
}
