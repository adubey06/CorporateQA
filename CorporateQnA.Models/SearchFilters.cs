using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Models
{
    public class SearchFilters
    {
        public string SearchKey { get; set; }
        public long CategoryId { get; set; }
        public ShowOption Show { get; set; }
        public SortByOption SortBy { get; set; }
    }
}
