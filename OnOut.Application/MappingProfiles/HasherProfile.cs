using AutoMapper;
using OnOut.Application.Features.Hasher.Commands.CreateCommand;
using OnOut.Application.Features.Hasher.Queries.GetAll;
using OnOut.Application.Features.Hasher.Queries.GetWithDetails;
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
            CreateMap<HasherListDto, Hasher>().ReverseMap();
            CreateMap<HasherDetailsDto, Hasher>().ReverseMap();
            CreateMap<CreateHasherCommand, Hasher>().ReverseMap();
        }
    }
}
