using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthentificationService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>().ConstructUsing(v => new UserViewModel(v));
            //CreateMap<UserViewModel, User>();
        }
    }
}
