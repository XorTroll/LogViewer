using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogViewer.BinaryLog
{
    public class LogPacket
    {
        public LogPacketHeader Header { get; set; }

        public List<LogDataChunk> Chunks { get; set; }

        public LogPacket()
        {
            Header = new LogPacketHeader();
            Chunks = new List<LogDataChunk>();
        }
    }
}
