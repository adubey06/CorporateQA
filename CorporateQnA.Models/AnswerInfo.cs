using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Models
{
    public class AnswerInfo
    {
        public long Id { get; set; }
        public long AnsweredFor { get; set; }
        public long AnsweredById { get; set; }
        public string AnsweredBy { get; set; }
        public string ProfileImage { get; set; }
        public string Description { get; set; }
        public long Likes { get; set; }
        public long Dislikes { get; set; }
        public string AnsweredOn { get; set; }
        public short Status { get; set; }
        public bool Best { get; set; }

    }
}
