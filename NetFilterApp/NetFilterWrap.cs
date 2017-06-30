using System;
using System.Runtime.InteropServices;

namespace NetFilterApp
{
    public static class NetFilterWrap
    {
        public static IntPtr Create()
        {
            return SafeNativeMethods.NetMonCreate();
        }

        public static void Free(IntPtr pNetMon)
        {
            SafeNativeMethods.NetMonFree(pNetMon);
        }

        public static bool Start(IntPtr pNetMon)
        {
            return Convert.ToBoolean(SafeNativeMethods.NetMonStart(pNetMon));
        }

        public static bool Started(IntPtr pNetMon)
        {
            return Convert.ToBoolean(SafeNativeMethods.NetMonIsStarted(pNetMon));
        }

        public static void Stop(IntPtr pNetMon)
        {
            SafeNativeMethods.NetMonStop(pNetMon);
        }

        public static void RefreshSetting(IntPtr pNetMon)
        {
            SafeNativeMethods.NetMonRefreshSettings(pNetMon);
        }

        public static void LogPath(IntPtr pNetMon, byte[] logPath, uint size)
        {
            SafeNativeMethods.NetMonLogPath(pNetMon, logPath, size);
        }

        public static void DeleteHttpRequestDumpFolder(IntPtr pNetMon)
        {
            SafeNativeMethods.NetMonDeleteHttpRequestDumpFolder(pNetMon);
        }

        public static void DeleteHttpResponseDumpFolder(IntPtr pNetMon)
        {
            SafeNativeMethods.NetMonDeleteHttpResponseDumpFolder(pNetMon);
        }

        public static void DeleteRawInDumpFolder(IntPtr pNetMon)
        {
            SafeNativeMethods.NetMonDeleteRawInDumpFolder(pNetMon);
        }

        public static void DeleteRawOutDumpFolder(IntPtr pNetMon)
        {
            SafeNativeMethods.NetMonDeleteRawOutDumpFolder(pNetMon);
        }
    }

    internal static class SafeNativeMethods
    {
        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr NetMonCreate();

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NetMonFree(IntPtr pNetMon);

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int NetMonStart(IntPtr pNetMon);

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int NetMonIsStarted(IntPtr pNetMon);

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NetMonStop(IntPtr pNetMon);

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NetMonRefreshSettings(IntPtr pNetMon);

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NetMonLogPath(IntPtr pNetMon, byte[] logPath, uint size);

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NetMonDeleteHttpRequestDumpFolder(IntPtr pNetMon);

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NetMonDeleteHttpResponseDumpFolder(IntPtr pNetMon);

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NetMonDeleteRawInDumpFolder(IntPtr pNetMon);

        [DllImport("NetFilter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NetMonDeleteRawOutDumpFolder(IntPtr pNetMon);
    }
}
