using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Exceptions
{
    public class NotFound:Exception
    {
        public NotFound(string name, object key) : base($"{name} ({key}) was not found") { }
    }
}
