using AutoMapper;
using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Mapper
{
    public class ActivityAutomapperProfile: Profile
    {
        public ActivityAutomapperProfile()
        {
            CreateMap<AnswerActivity, Data.AnswerActivity>()
                .ForMember(dest=> dest.Type, opt=> opt.MapFrom(src=> (short)src.Type));
            CreateMap<QuestionActivity, Data.QuestionActivity>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (short)src.Type));
            CreateMap<Data.QuestionActivity, QuestionActivity>()
                .ForMember(dest=>dest.Type, opt=> opt.MapFrom(src=>(Activity)src.Type));
            CreateMap<Data.AnswerActivity, AnswerActivity>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (Activity)src.Type));
        }
    }
}
