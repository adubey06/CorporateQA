using System.ComponentModel.DataAnnotations;

namespace CorporateQnA.Models
{
    public class UserDetail
    {
        public string Name { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
