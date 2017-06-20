using System;
using System.Runtime.InteropServices;

namespace NetFilterApp
{
    public static class NetFilterWrap
    {
        public static IntPtr NetMonCreate()
        {
            return SafeNativeMethods.NetMonCreate();
        }

        public static void NetMonFree(IntPtr pNetMon)
        {
            SafeNativeMethods.NetMonFree(pNetMon);
        }

        public static bool NetMonStart(IntPtr pNetMon)
        {
            return SafeNativeMethods.NetMonStart(pNetMon);
        }

        public static bool NetMonIsStarted(IntPtr pNetMon)
        {
            return SafeNativeMethods.NetMonIsStarted(pNetMon);
        }

        public static void NetMonStop(IntPtr pNetMon)
        {
            SafeNativeMethods.NetMonStop(pNetMon);
        }

        public static void NetMonRefreshSetting(IntPtr pNetMon)
        {
            SafeNativeMethods.NetMonRefreshSettings(pNetMon);
        }

        public static void NetMonLogPath(IntPtr pNetMon, byte[] logPath, uint size)
        {
            SafeNativeMethods.NetMonLogPath(pNetMon, logPath, size);
        }
    }

    internal static class SafeNativeMethods
    {
        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr NetMonCreate();

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NetMonFree(IntPtr pNetMon);

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool NetMonStart(IntPtr pNetMon);

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool NetMonIsStarted(IntPtr pNetMon);

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NetMonStop(IntPtr pNetMon);

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NetMonRefreshSettings(IntPtr pNetMon);

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern string NetMonLogPath(IntPtr pNetMon, byte[] logPath, uint size);
    }
}
