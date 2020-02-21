using AutoMapper;
using CorporateQnA.Models;
using CorporateQnA.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Mapper
{
    public class AnswerAutomapperProfile:Profile
    {
        public AnswerAutomapperProfile()
        {
            CreateMap<Answer, Data.Answer>().ReverseMap();
            CreateMap<Data.AnswerInfo, AnswerInfo>()
                .ForMember(dest => dest.AnsweredOn, opt => opt.MapFrom(src => src.AnsweredOn.getValue()));
        }
    }

   
}
