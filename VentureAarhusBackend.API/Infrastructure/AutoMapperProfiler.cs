using AutoMapper;
using VentureAarhusBackend.API.DTOs;
using VentureAarhusBackend.API.Models;

namespace VentureAarhusBackend.API.Infrastructure
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
