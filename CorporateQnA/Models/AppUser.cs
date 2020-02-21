using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateQnA.Models
{
    public class AppUser:IdentityUser<long>
    {
        public long UserId { get; set; }
    }
}
