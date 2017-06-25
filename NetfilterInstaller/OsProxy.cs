using System;
using System.Management;
using System.Security.Principal;

namespace NetfilterInstaller
{
    static class OsProxy
    {
        public static string GetOsVersion()
        {
            string result = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                "SELECT Caption FROM Win32_OperatingSystem");
            foreach (ManagementObject os in searcher.Get())
            {
                result = os["Caption"].ToString();
                result = result.ToLower();
                break;
            }

            //Bullshit code.. TODO: Refactor
            if (result.Contains("windows"))
            {
                string tmp = "windows";
                if (result.Contains("7"))
                {
                    tmp += 7;
                }
                else if (result.Contains("8"))
                {
                    tmp += 8;
                }
                else if (result.Contains("10"))
                {
                    tmp += 8;
                }
                else
                {
                    return string.Empty;
                }

                result = tmp;
            }

            return result;
        }

        public static bool CurrentUserIsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
