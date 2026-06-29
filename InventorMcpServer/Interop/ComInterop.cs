using System;
using System.Runtime.InteropServices;
using System.Security;

namespace InventorMcpServer.Interop;

internal static class ComInterop
{
    [DllImport("ole32.dll", PreserveSig = false)]
    [SuppressUnmanagedCodeSecurity]
    private static extern void CLSIDFromProgIDEx(
        [MarshalAs(UnmanagedType.LPWStr)] string progId,
        out Guid clsid);

    [DllImport("ole32.dll", PreserveSig = false)]
    [SuppressUnmanagedCodeSecurity]
    private static extern void CLSIDFromProgID(
        [MarshalAs(UnmanagedType.LPWStr)] string progId,
        out Guid clsid);

    [DllImport("oleaut32.dll", PreserveSig = false)]
    [SuppressUnmanagedCodeSecurity]
    private static extern void GetActiveObject(
        ref Guid rclsid,
        IntPtr reserved,
        [MarshalAs(UnmanagedType.Interface)] out object obj);

    public static object GetRunningInstance(string progId)
    {
        Guid clsid;

        try
        {
            CLSIDFromProgIDEx(progId, out clsid);
        }
        catch
        {
            CLSIDFromProgID(progId, out clsid);
        }

        GetActiveObject(ref clsid, IntPtr.Zero, out object obj);

        return obj;
    }
}