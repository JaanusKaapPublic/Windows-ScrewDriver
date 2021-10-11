using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyblikGUI
{
    public class RBLK
    {
        public struct RblkMessageStruct
        {
            public String driverName;
            public String deviceName;
            public uint code;
            public byte[] input;
            public uint outputSize;
        }

        static public List<RblkMessageStruct> open(String fname)
        {
            List<RblkMessageStruct> result = new List<RblkMessageStruct>();
            using (BinaryReader reader = new BinaryReader(File.Open(fname, FileMode.Open)))
            {
                if (reader.ReadInt32() != 0x4b4c4252)
                    return null;

                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    RblkMessageStruct res = new RblkMessageStruct();
                    ushort driverNameLen = reader.ReadUInt16();
                    ushort deviceNameLen = reader.ReadUInt16();
                    res.code = reader.ReadUInt32();
                    uint inputSize = reader.ReadUInt32();
                    res.outputSize = reader.ReadUInt32();

                    if (driverNameLen > 0)
                        res.driverName = Encoding.Unicode.GetString(reader.ReadBytes(driverNameLen));
                    else
                        res.driverName = "<NO NAME>";

                    if (deviceNameLen > 0)
                        res.deviceName = Encoding.Unicode.GetString(reader.ReadBytes(deviceNameLen));
                    else
                        res.deviceName = "<NO NAME>";

                    if (inputSize > 0)
                        res.input = reader.ReadBytes((int)inputSize);
                    else
                        res.input = new byte[0];
                    result.Add(res);
                }
            }
            return result;
        }

        static public bool save(String fname, List<RblkMessageStruct> data)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(fname, FileMode.Create)))
            {
                writer.Write(0x4b4c4252);

                foreach(RblkMessageStruct call in data)
                {
                    byte[] driverName = Encoding.Unicode.GetBytes(call.driverName);
                    byte[] deviceName = Encoding.Unicode.GetBytes(call.deviceName);
                    writer.Write((UInt16)driverName.Length);
                    writer.Write((UInt16)deviceName.Length);
                    writer.Write(call.code);
                    writer.Write(call.input.Length);
                    writer.Write(call.outputSize);
                    writer.Write(driverName);
                    writer.Write(deviceName);
                    writer.Write(call.input);
                }
            }
            return true;
        }
    }
}
