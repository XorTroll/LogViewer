using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LogViewer.BinaryLog
{
    public enum LogDataChunkKey : ulong
    {
        LogSessionBegin,
        LogSessionEnd,
        TextLog,
        LineNumber,
        FileName,
        FunctionName,
        ModuleName,
        ThreadName,
        LogPacketDropCount,
        UserSystemClock,
        ProcessName
    }

    public class LogDataChunk : IReadable
    {
        public LogDataChunkKey Key { get; set; }

        public byte[] Data { get; set; }

        public void ReadFrom(BinaryReader reader)
        {
            var key_v = reader.BaseStream.ReadLEB128Unsigned();
            Utils.Assert(Enum.IsDefined(typeof(LogDataChunkKey), key_v), $"Invalid LogDataChunkKey value: {key_v}");
            Key = (LogDataChunkKey)key_v;
            var size_v = reader.BaseStream.ReadLEB128Unsigned();
            Data = reader.ReadBytes((int)size_v);
        }

        public byte ReadU8()
        {
            return Data[0];
        }

        public uint ReadU32()
        {
            return BitConverter.ToUInt32(Data, 0);
        }

        public ulong ReadU64()
        {
            return BitConverter.ToUInt64(Data, 0);
        }

        public string ReadString()
        {
            return Encoding.UTF8.GetString(Data);
        }
    }
}
