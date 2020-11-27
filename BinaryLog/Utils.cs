using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogViewer.BinaryLog
{
    public class AssertionException : Exception
    {
        public AssertionException(string msg) : base("Assertion failed: " + msg) { }
    }

    public static class Utils
    {
        public static void Assert(bool condition, string msg)
        {
            if (!condition) throw new AssertionException(msg);
        }
    }
}
