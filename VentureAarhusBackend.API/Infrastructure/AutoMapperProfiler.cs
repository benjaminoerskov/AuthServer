using AutoMapper;
using DetSkerIAarhusBackend.API.DTOs;
using DetSkerIAarhusBackend.API.Models;

namespace DetSkerIAarhusBackend.API.Infrastructure
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<ApplicationUser, GetUserDTO>().ReverseMap()
                .ForMember(p => p.Id, opt => opt.Ignore());

            CreateMap<AssociatedOccurrences, AssociatedOccurrencesDTO>().ReverseMap();
        }
    }
}
