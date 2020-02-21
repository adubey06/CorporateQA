using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Models
{
    public class CategoryInfo: Category
    {
        public long TotalTagged { get; set; }
        public long WeeklyTagged { get; set; }
        public long MonthlyTagged { get; set; }
        public string CreatedByPersonName { get; set; }
    }
}
