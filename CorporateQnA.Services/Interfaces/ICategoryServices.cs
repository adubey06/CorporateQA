using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Interfaces
{
    public interface ICategoryServices
    {
        IEnumerable<CategoryInfo> GetAllCategories();
        public Category AddCategory(Category category);
    }
}
