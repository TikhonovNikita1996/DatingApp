using System;
using API.DTOs;
using API.Entities;
using API.Extentions;
using AutoMapper;

namespace API.Helpers;

public class AutomapperProfiles : Profile
{
    public AutomapperProfiles()
    {
        CreateMap<AppUser, MemberDto>()
        .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge()))
        .ForMember(d=> d.PhotoUrl, o => 
            o.MapFrom(s => s.Photos.FirstOrDefault(x=>x.IsMain)!.Url));

        CreateMap<Photo, PhotoDto>();
        CreateMap<MemberUpdateDto, AppUser>();
    }
}
