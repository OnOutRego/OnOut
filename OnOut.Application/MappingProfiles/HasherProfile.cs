using AutoMapper;
using OnOut.Application.Features.Hasher;
using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.MappingProfiles
{
    public class HasherProfile: Profile
    {
        public HasherProfile()
        {
            CreateMap<HasherDto, Hasher>().ReverseMap();
        }
    }
}
