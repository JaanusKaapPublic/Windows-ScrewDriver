using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RyblikGUI
{
    class Driver
    {
        [DllImport("RyblikDll.dll", EntryPoint = "init", CallingConvention = CallingConvention.StdCall)]
        static extern bool init();
        [DllImport("RyblikDll.dll", EntryPoint = "getDriverCount", CallingConvention = CallingConvention.StdCall)]
        static extern int getDriverCountDll();
        [DllImport("RyblikDll.dll", EntryPoint = "getDriverAddresses", CallingConvention = CallingConvention.StdCall)]
        static unsafe extern uint getDriverAddressesDll(long* data, int size);
        [DllImport("RyblikDll.dll", EntryPoint = "getDriverName", CallingConvention = CallingConvention.StdCall)]
        static unsafe extern uint getDriverNameDll(long addr, byte* data, int size);
        [DllImport("RyblikDll.dll", EntryPoint = "getDriverDeviceCount", CallingConvention = CallingConvention.StdCall)]
        static extern int getDriverDeviceCountDll(long addr);
        [DllImport("RyblikDll.dll", EntryPoint = "getDriverDeviceAddresses", CallingConvention = CallingConvention.StdCall)]
        static unsafe extern uint getDriverDeviceAddressesDll(long addr, long* data, int size);
        [DllImport("RyblikDll.dll", EntryPoint = "getDriverDeviceName", CallingConvention = CallingConvention.StdCall)]
        static unsafe extern uint getDriverDeviceNameDll(long addr, long devAddr, byte* data, int size);
        [DllImport("RyblikDll.dll", EntryPoint = "getDriverMajorFunctions", CallingConvention = CallingConvention.StdCall)]
        static unsafe extern uint getDriverMajorFunctionsDll(long addr, long* data, int size);
        [DllImport("RyblikDll.dll", EntryPoint = "devCtrlReq", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        static unsafe extern int devCtrlReqDll(String driver, uint access, uint code, byte* inBuffer, uint inBufferSize, byte* outBuffer, uint outBufferSize);
        [DllImport("RyblikDll.dll", EntryPoint = "hookDriver", CallingConvention = CallingConvention.StdCall)]
        public static extern bool hookDriver(long drv, long dev);
        [DllImport("RyblikDll.dll", EntryPoint = "unhookDriver", CallingConvention = CallingConvention.StdCall)]
        public static extern bool unhookDriver(long drv, long dev);
        [DllImport("RyblikDll.dll", EntryPoint = "isHookedDriver", CallingConvention = CallingConvention.StdCall)]
        public static extern bool isHookedDriver(long drv, long dev);
        [DllImport("RyblikDll.dll", EntryPoint = "setHookConfValue", CallingConvention = CallingConvention.StdCall)]
        public static extern bool setHookConfValue(long drv, long dev, uint type, uint value);
        [DllImport("RyblikDll.dll", EntryPoint = "getHookConfValue", CallingConvention = CallingConvention.StdCall)]
        public static extern uint getHookConfValue(long drv, long dev, uint type);
        [DllImport("RyblikDll.dll", EntryPoint = "startFileLog", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        static extern bool startFileLogDll(String driver, int size);
        [DllImport("RyblikDll.dll", EntryPoint = "stopFileLog", CallingConvention = CallingConvention.StdCall)]
        public static extern bool stopFileLog();
        [DllImport("RyblikDll.dll", EntryPoint = "isFileLogActivated", CallingConvention = CallingConvention.StdCall)]
        public static extern bool isFileLogActivated();


        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern long CreateFile( string lpFileName, uint dwDesiredAccess, uint dwShareMode, uint lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, uint hTemplateFile );
        [DllImport("kernel32.dll")]
        static extern uint GetLastError();
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int CloseHandle(long handle);

        static public bool initialize()
        {
            return init();
        }
               
        static public int getDriverCount()
        {
            return getDriverCountDll();
        }

        static public long[] getDriverAddresses()
        {
            unsafe
            {
                int count = getDriverCount();
                if (count == -1)
                    return new long[0];
                long[] data = new long[count];
                fixed (long* tmp = data)
                {
                    if (getDriverAddressesDll(tmp, count * IntPtr.Size) == 0xFFFFFFFF)
                        return null;
                }
                return data;
            }
        }

        static public String getDriverName(long addr)
        {
            unsafe
            {
                byte[] name = new byte[2048];
                fixed (byte* tmp = name)
                {
                    uint count = getDriverNameDll(addr, tmp, 2048);
                    if (count == 0xFFFFFFFF || count == 0)
                        return null;                    
                }
                return Encoding.Unicode.GetString(name);
            }
        }

        static public int getDriverDeviceCount(long addr)
        {
            return getDriverDeviceCountDll(addr);
        }

        static public long[] getDriverDeviceAddresses(long addr)
        {
            unsafe
            {
                int count =  getDriverDeviceCount(addr);
                long[] data = new long[count];
                fixed (long* tmp = data)
                {
                    if (getDriverDeviceAddressesDll(addr, tmp, count * IntPtr.Size) == 0xFFFFFFFF)
                        return null;
                }
                return data;
            }
        }

        static public String getDriverDeviceName(long addr, long addrDev)
        {
            unsafe
            {
                byte[] name = new byte[2048];
                uint count;
                fixed (byte* tmp = name)
                {
                    count = getDriverDeviceNameDll(addr, addrDev, tmp, 2048);
                    if (count == 0xFFFFFFFF || count == 0)
                        return null;
                }
                return Encoding.Unicode.GetString(name, 0, (int)count);
            }
        }

        
        static public long[] getDriverMajorFunctions(long addr)
        {
            unsafe
            {
                long[] data = new long[0x1C];
                fixed (long* tmp = data)
                {
                    if (getDriverMajorFunctionsDll(addr, tmp, 0x1C * IntPtr.Size) == 0xFFFFFFFF)
                        return null;
                }
                return data;
            }
        }

        static public bool openable(String device, bool read, bool write)
        {
            device = "\\\\?\\GLOBALROOT" + device;
            long h = CreateFile(device, (uint)(read ? 0x80000000 : 0) | (uint)(write ? 0x40000000 : 0), 0, 0, 0x3, 0x00000080, 0);
            if (h != -1)
                CloseHandle(h);
            return (h != -1);
        }

        static public uint devCtrlReq(String driver, uint access, uint code, byte[] inBuffer,out byte[] outBuffer, uint outBufferSize)
        {
            unsafe
            {
                byte[] outBufferTmp = new byte[outBufferSize];

                fixed (byte* bufferIn = inBuffer)
                {
                    fixed (byte* bufferOut = outBufferTmp)
                    {
                        int len = devCtrlReqDll(driver, access, code, bufferIn, (uint)inBuffer.Length, bufferOut, outBufferSize);
                        if (len == -1)
                        {
                            outBuffer = null;
                            return GetLastError();
                        }
                        outBuffer = new byte[len];
                        Array.Copy(outBufferTmp, 0, outBuffer, 0, len);
                        return 0;
                    }
                }
            }
        }

        public static bool startFileLog(String driver)
        {
            return startFileLogDll(driver, driver.Length * 2);
        }
    }
}
