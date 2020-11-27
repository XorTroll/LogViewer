using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LogViewer.BinaryLog
{
    [Flags]
    public enum LogPacketFlags : byte
    {
        Head = 1,
        Tail = 2,
        LittleEndian = 4
    }

    public enum LogSeverity : byte
    {
        Trace,
        Info,
        Warn,
        Error,
        Fatal
    }

    public class LogPacketHeader : IReadable
    {
        public ulong ProcessId { get; set; }

        public ulong ThreadId { get; set; }

        public LogPacketFlags Flags { get; set; }

        public LogSeverity Severity { get; set; }

        public byte Verbosity { get; set; }

        public uint PayloadSize { get; set; }

        public void ReadFrom(BinaryReader reader)
        {
            ProcessId = reader.ReadUInt64();
            ThreadId = reader.ReadUInt64();
            Flags = (LogPacketFlags)reader.ReadByte();
            // Padding/reserved
            reader.ReadByte();
            var severity_v = reader.ReadByte();
            Utils.Assert(Enum.IsDefined(typeof(LogSeverity), severity_v), $"Invalid LogSeverity value: {severity_v}");
            Severity = (LogSeverity)severity_v;
            Verbosity = reader.ReadByte();
            PayloadSize = reader.ReadUInt32();
        }

        public bool IsHead()
        {
            return (Flags & LogPacketFlags.Head) != 0;
        }

        public bool IsTail()
        {
            return (Flags & LogPacketFlags.Tail) != 0;
        }
    }
}
