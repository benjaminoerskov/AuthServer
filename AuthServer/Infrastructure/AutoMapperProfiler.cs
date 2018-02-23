using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.DTOs;
using AuthServer.Models;
using AutoMapper;

namespace AuthServer.Infrastructure
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<ApplicationUser, GetUserDTO>().ReverseMap()
                .ForMember(p => p.Id, opt => opt.Ignore());
        }
    }
}
