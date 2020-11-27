using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LogViewer.BinaryLog
{
    public class BinaryLogHeader : IReadable
    {
        public const uint MagicValue = 0x70687068; // "hphp"

        public uint Magic { get; set; }

        public byte Version { get; set; }

        public void ReadFrom(BinaryReader reader)
        {
            Magic = reader.ReadUInt32();
            Utils.Assert(Magic == MagicValue, $"Invalid BinaryLogHeader magic: {Magic}");

            Version = reader.ReadByte();
            // Padding/reserved
            reader.ReadBytes(3);
        }
    }
}
