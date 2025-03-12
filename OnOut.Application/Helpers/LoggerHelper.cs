using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Helpers
{
    public class LoggerHelper
    {
        public static string GetLastSegment(string fullname)
        {
            string[] segments = fullname.Split('.');
            return (segments[segments.Length - 1]);
        }
    }
}
