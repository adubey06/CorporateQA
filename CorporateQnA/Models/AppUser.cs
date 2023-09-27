using Microsoft.AspNetCore.Identity;

namespace CorporateQnA.Models
{
    public class AppUser:IdentityUser<long>
    {
        public long UserId { get; set; }
    }
}
