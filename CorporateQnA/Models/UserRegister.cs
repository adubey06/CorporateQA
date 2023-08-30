using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class UserRegister
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string Role { get; set; }
        public string Location { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
