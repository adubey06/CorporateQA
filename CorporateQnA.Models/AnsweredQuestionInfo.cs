using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Models
{
    public class AnsweredQuestionInfo
    {
        public long Id { get; set; }
        public string ProfileImage { get; set; }
        public long AskedById { get; set; }
        public string AskedBy { get; set; }
        public string Title { get; set; }
        public string QuestionDescription { get; set; }
        public long Upvotes { get; set; }
        public long TotalAnswers { get; set; }
        public long TotalViews { get; set; }
        public long AnswerId { get; set; }
        public long AnsweredById { get; set; }
        public string Description { get; set; }
        public string AnsweredOn { get; set; }
        public short Status { get; set; }
    }
}
