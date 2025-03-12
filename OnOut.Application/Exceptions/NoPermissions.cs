using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Exceptions
{
    public class NoPermissions : Exception
    {
        //public NotFound(string name, object key) : base($"{name} ({key}) was not found") { }
        public NoPermissions(string name, object key) : base($"You do not have Permission to do this action {name} {key}") { }
    }
}
