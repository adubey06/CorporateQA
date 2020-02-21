using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Data
{
    public class QuestionActivity
    {
        public long Id { get; set; }
        public long ActivityBy { get; set; }
        public long ActivityPerformedOn { get; set; }
        public short Type { get; set; }
        public DateTime ActivityOn { get; set; }
    }
}
