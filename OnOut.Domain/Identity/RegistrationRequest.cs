using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Domain.Identity
{
    public class RegistrationRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        [Required]
        public string Password { get; set; }

        [MinLength(6)]
        [Required]
        public string UserName { get; set; }

    }
}
