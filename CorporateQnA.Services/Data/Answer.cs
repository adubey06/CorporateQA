using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Data
{
    public class Answer
    {
        public long Id { get; set; }
        public long QuestionId { get; set; }
        public long AnsweredBy { get; set; }
        public string Description { get; set; }
        public DateTime AnsweredOn { get; set; }
    }
}
