using AutoMapper;
using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Mapper
{
    public class CategoryAutomapperProfile:Profile
    {
        public CategoryAutomapperProfile()
        {
            CreateMap<Category, Data.Category>().ReverseMap();
            CreateMap<CategoryInfo, Data.CategoryInfo>().ReverseMap();
        }
    }
}
