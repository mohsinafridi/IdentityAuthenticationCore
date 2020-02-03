using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthenticationCore.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Password { get; set; }
    }
}
