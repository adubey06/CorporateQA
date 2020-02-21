using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CorporateQnA.Services.Mapper;

namespace CorporateQnA.Services.Extensions
{
    public class AutoMapperConfiguration
    {
        public IMapper GetConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserAutomapperProfile>();
                cfg.AddProfile<CategoryAutomapperProfile>();
                cfg.AddProfile<QuestionAutomapperProfile>();
                cfg.AddProfile<ActivityAutomapperProfile>();
                cfg.AddProfile<AnswerAutomapperProfile>();
            }).CreateMapper();
        }
    }
}
