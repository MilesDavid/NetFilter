using System.Security.Principal;

namespace NetfilterInstaller
{
    static class OsProxy
    {

        public static string GetOsVersion()
        {
            string result = "";
            return result;
        }

        public static bool Is64OsBit()
        {
            bool result = false;
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
