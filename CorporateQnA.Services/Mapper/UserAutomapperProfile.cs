using AutoMapper;
using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Mapper
{
    public class UserAutomapperProfile: Profile
    {
        public UserAutomapperProfile()
        {
            CreateMap<User, Data.User>().ReverseMap();
            CreateMap<UserInfo, Data.UserInfo>().ReverseMap();
            
        }
    }
}
