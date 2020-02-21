using System;

namespace CorporateQnA.Models
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
