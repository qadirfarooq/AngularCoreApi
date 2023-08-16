using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using API.Entities;
using API.DTO;
using API.Extensions;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser , MemberDto>()
            .ForMember(dest=> dest.PhotoUrl, option=> option.MapFrom(source=> source.Photos.FirstOrDefault(a=> a.IsMain).Url))
            .ForMember(dest=> dest.Age , opt=> opt.MapFrom( source=> source.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();

            CreateMap<MemberUpdateDto,AppUser>();

        }
    }
}