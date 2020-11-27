using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LogViewer.BinaryLog
{
    public interface IReadable
    {
        void ReadFrom(BinaryReader reader);
    }
}
