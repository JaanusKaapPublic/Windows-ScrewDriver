using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RyblikGUI.RBLK;

namespace RyblikGUI
{
    class ExportPython
    {
        public static String exportSingle(String device, UInt32 access, UInt32 code, byte[] input, UInt32 outSize)
        {
            String res = "from win32file import *\n";
            res += "h = CreateFile(\"\\\\\\\\?\\\\GLOBALROOT" + device.Replace("\\", "\\\\") + "\", 0x" + access.ToString("X") + ", 0, None, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, None)\n";
            res += "buf = \"";
            foreach (byte b in input)
                res += "\\x" + b.ToString("X2");
            res += "\"\n";
            res += "print DeviceIoControl(h, 0x" + code.ToString("X") + ", buf, 0x" + outSize.ToString("X") + ", None)\n";
            return res;
        }
        public static String exportMultiple(List<RblkMessageStruct> calls, System.IO.FileStream fout)
        {
            String res = "from win32file import *\n\n";
            res += "requests = [\n";
            byte[] bytes = Encoding.UTF8.GetBytes(res);
            fout.Write(bytes, 0, bytes.Length);

            foreach (RblkMessageStruct call in calls)
            {
                uint access = (call.code >> 14) & 0x3;
                res = "  [";
                res += "\"" + call.deviceName.Replace("\\", "\\\\") + "\", ";
                res += "0x" + access.ToString("X") + ", ";
                res += "0x" + call.code.ToString("X") + ", ";
                res += "\"";
                foreach (byte b in call.input)
                    res += "\\x" + b.ToString("X2");
                res += "\", ";
                res += "0x" + call.outputSize.ToString("X") + ", ";
                res += "],\n";
                bytes = Encoding.UTF8.GetBytes(res);
                fout.Write(bytes, 0, bytes.Length);
            }

            res = "]\n\n";
            res += "for req in requests:\n";
            res += "\ttry:\n";
            res += "\t\th = CreateFile(\"\\\\\\\\?\\\\GLOBALROOT\" + req[0], req[1], 0, None, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, None)\n";
            res += "\t\tprint DeviceIoControl(h, req[2], req[3], req[4], None)\n";
            res += "\texcept:\n";
            res += "\t\tprint \"Failed\"\n";
            bytes = Encoding.UTF8.GetBytes(res);
            fout.Write(bytes, 0, bytes.Length);

            return res;
        }
    }
}
