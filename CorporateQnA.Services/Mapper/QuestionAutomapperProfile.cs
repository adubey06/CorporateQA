using AutoMapper;
using CorporateQnA.Models;
using CorporateQnA.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Mapper
{
    public class QuestionAutomapperProfile: Profile
    {
        public QuestionAutomapperProfile()
        {
            CreateMap<Question, Data.Question>().ReverseMap();
            CreateMap<Data.QuestionInfo, QuestionInfo>()
                .ForMember(dest => dest.AskedOn, opt => opt.MapFrom(src => src.AskedOn.getValue()));
            CreateMap<Data.AnsweredQuestionInfo, AnsweredQuestionInfo>()
                .ForMember(dest=>dest.AnsweredOn , opt=> opt.MapFrom(src => src.AnsweredOn.getValue()));
        }
    }
}
