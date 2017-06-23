using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace NetfilterInstaller
{
    static class DriverInstaller
    {
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

            string srcDriverPath = "";

            if (driverType == DriverType.WFP)
            {

                srcDriverPathDict.Add("driverVersion", "wfp");
                srcDriverPathDict.Add("osVersion", OsProxy.GetOsVersion());
            }
            else
            {
                srcDriverPathDict.Add("driverVersion", "tdi");
            }

            srcDriverPathDict.Add("bitCapasity", (OsProxy.Is64OsBit()) ? "i386" : "amd64");
            srcDriverPathDict.Add("driverName", "netfilter2.sys");

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

            return true;
        }
    }
}
