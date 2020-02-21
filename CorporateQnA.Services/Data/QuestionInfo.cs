using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Data
{
    public class QuestionInfo
    {
        public long Id { get; set; }
        public long AskedById { get; set; }
        public string AskedBy { get; set; }
        public string ProfileImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public long Upvotes { get; set; }
        public long TotalViews { get; set; }
        public long TotalAnswers { get; set; }
        public DateTime AskedOn { get; set; }
        public short Resolved { get; set; }
    }
}
