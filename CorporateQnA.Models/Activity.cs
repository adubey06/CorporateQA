using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Models
{
    public class ActivityModel
    {
        public long Id { get; set; }
        public long ActivityBy { get; set; }
        public long ActivityPerformedOn { get; set; }
        public Activity Type { get; set; }
        public DateTime ActivityOn { get; set; }
    }
    public class QuestionActivity: ActivityModel
    {

    }
    
    public class AnswerActivity: ActivityModel
    {

    }
}
