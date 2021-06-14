using AutoMapper;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Helpers
{
    public class autoMapperProfiles : Profile
    {
        public autoMapperProfiles()
        {
            CreateMap<appUser, memberDTO>()
                .ForMember(dest => dest.photoUrl, opt => opt.MapFrom(src => src.photos.FirstOrDefault(x => x.isMain).url))
                .ForMember(dest => dest.age, opt => opt.MapFrom(src => src.DateOfBirth.calculateAge()));
            CreateMap<photo, photoDTO>();
            CreateMap<memberUpdateDTO, appUser>();
            CreateMap<registerDTO, appUser>()
                .ForMember(dest=>dest.password,o=>o.Ignore());
        }
    }
}
