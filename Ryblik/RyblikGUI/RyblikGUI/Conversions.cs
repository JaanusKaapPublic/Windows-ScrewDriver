using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyblikGUI
{
    class Conversions
    {
        public static String majorFuncName(int idx)
        {
            switch (idx)
            {
                case 0:
                    return "IRP_MJ_CREATE";
                case 0x1:
                    return "IRP_MJ_CREATE_NAMED_PIPE";
                case 0x2:
                    return "IRP_MJ_CLOSE";
                case 0x3:
                    return "IRP_MJ_READ";
                case 0x4:
                    return "IRP_MJ_WRITE";
                case 0x5:
                    return "IRP_MJ_QUERY_INFORMATION";
                case 0x6:
                    return "IRP_MJ_SET_INFORMATION";
                case 0x7:
                    return "IRP_MJ_QUERY_EA";
                case 0x8:
                    return "IRP_MJ_SET_EA";
                case 0x9:
                    return "IRP_MJ_FLUSH_BUFFERS";
                case 0xA:
                    return "IRP_MJ_QUERY_VOLUME_INFORMATION";
                case 0xB:
                    return "IRP_MJ_SET_VOLUME_INFORMATION";
                case 0xC:
                    return "IRP_MJ_DIRECTORY_CONTROL";
                case 0xD:
                    return "IRP_MJ_FILE_SYSTEM_CONTROL";
                case 0xE:
                    return "IRP_MJ_DEVICE_CONTROL";
                case 0xF:
                    return "IRP_MJ_INTERNAL_DEVICE_CONTROL";
                case 0x10:
                    return "IRP_MJ_SHUTDOWN";
                case 0x11:
                    return "IRP_MJ_LOCK_CONTROL";
                case 0x12:
                    return "IRP_MJ_CLEANUP";
                case 0x13:
                    return "IRP_MJ_CREATE_MAILSLOT";
                case 0x14:
                    return "IRP_MJ_QUERY_SECURITY";
                case 0x15:
                    return "IRP_MJ_SET_SECURITY";
                case 0x16:
                    return "IRP_MJ_POWER";
                case 0x17:
                    return "IRP_MJ_SYSTEM_CONTROL";
                case 0x18:
                    return "IRP_MJ_DEVICE_CHANGE";
                case 0x19:
                    return "IRP_MJ_QUERY_QUOTA";
                case 0x1a:
                    return "IRP_MJ_SET_QUOTA";
                case 0x1b:
                    return "IRP_MJ_PNP";
                default:
                    return "???";
            }
        }

        public static String errorNames(uint err)
        {
            switch (err)
            {
                case 0:
                    return "SUCCESS";
                case 0x1:
                    return "ERROR_INVALID_FUNCTION";
                case 0x2:
                    return "ERROR_FILE_NOT_FOUND";
                case 0x3:
                    return "ERROR_PATH_NOT_FOUND";
                case 0x5:
                    return "ERROR_ACCESS_DENIED";
                case 0x6:
                    return "ERROR_INVALID_HANDLE";
                case 0x8:
                    return "ERROR_NOT_ENOUGH_MEMORY";
                case 0xA:
                    return "ERROR_BAD_ENVIRONMENT";
                case 0xB:
                    return "ERROR_BAD_FORMAT";
                case 0xC:
                    return "ERROR_INVALID_ACCESS";
                case 0xD:
                    return "ERROR_INVALID_DATA";
                case 0x18:
                    return "ERROR_BAD_LENGTH";
                case 0x1F:
                    return "ERROR_GEN_FAILURE";
                case 0x32:
                    return "ERROR_NOT_SUPPORTED";
                case 0x57:
                    return "ERROR_INVALID_PARAMETER";
                case 0x7A:
                    return "ERROR_INSUFFICIENT_BUFFER";
                case 0x45D:
                    return "ERROR_GEN_FAILURE";
                case 0x490:
                    return "ERROR_NOT_FOUND";
                case 0x961:
                    return "ERROR_GEN_FAILURE";
                default:
                    return "UNKNOWN ERROR :S";
            }
        }

        public static String access(int val)
        {
            switch (val)
            {
                case 0:
                    return "NO ACCESS";
                case 0x1:
                    return "READ";
                case 0x2:
                    return "WRITE";
                case 0x3:
                    return "READ & WRITE";
                default:
                    return "???";
            }
        }

        public static int convert2int(String input)
        {
            input = input.Trim();
            if (input.StartsWith("0x"))
                return Convert.ToInt32(input, 16);
            else
                return Convert.ToInt32(input, 10);
        }
        public static uint convert2uint(String input)
        {
            input = input.Trim();
            if (input.StartsWith("0x"))
                return Convert.ToUInt32(input, 16);
            else
                return Convert.ToUInt32(input, 10);
        }
    }
}
