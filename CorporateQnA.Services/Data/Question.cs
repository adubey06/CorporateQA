using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Data
{
    public class Question
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public long AskedBy { get; set; }
        public DateTime AskedOn { get; set; }
    }
}
