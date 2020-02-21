using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Data
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Role { get; set; }
        public string Location { get; set; }
        public string ProfileImage { get; set; }
    }
}
