using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Identity.Models
{
    public class OnOutUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
