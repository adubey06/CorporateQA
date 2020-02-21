using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Data
{
    public class UserInfo
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string ProfileImage { get; set; }
        public string Designation { get; set; }
        public string Role { get; set; }
        public string Location { get; set; }
        public long Likes { get; set; }
        public long Dislikes { get; set; }
        public long QuestionsAsked { get; set; }
        public long QuestionsAnswered { get; set; }
        public long QuestionsSolved { get; set; }

    }
}
