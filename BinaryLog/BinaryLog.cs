using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LogViewer.BinaryLog
{
    public class BinaryLog
    {
        public BinaryLogHeader Header { get; set; }

        public List<LogPacket> Packets { get; set; }

        public BinaryLog()
        {
            Header = new BinaryLogHeader();
            Packets = new List<LogPacket>();
        }

        public static BinaryLog LoadFrom(string path)
        {
            var bin_log = new BinaryLog();
            using(var bin_reader = new BinaryReader(new FileStream(path, FileMode.Open)))
            {
                bin_log.Header.ReadFrom(bin_reader);
                while(bin_reader.BaseStream.Position != bin_reader.BaseStream.Length)
                {
                    var log_packet = new LogPacket();
                    log_packet.Header.ReadFrom(bin_reader);
                    var final_pos = bin_reader.BaseStream.Position + log_packet.Header.PayloadSize;
                    while(bin_reader.BaseStream.Position != final_pos)
                    {
                        var log_data_chunk = new LogDataChunk();
                        log_data_chunk.ReadFrom(bin_reader);
                        log_packet.Chunks.Add(log_data_chunk);
                    }
                    bin_log.Packets.Add(log_packet);
                }
            }
            return bin_log;
        }
    }
}
