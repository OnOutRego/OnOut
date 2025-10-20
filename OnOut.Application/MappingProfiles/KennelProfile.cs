using AutoMapper;
using OnOut.Application.Features.Kennel;
using OnOut.Application.Features.Kennel.Commands.CreateKennel;
using OnOut.Application.Features.KennelRoles.Commands.CreateKennelRole;
using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.MappingProfiles
{
    public class KennelProfile: Profile
    {
        public KennelProfile()
        {
            CreateMap<Kennel, KennelDto>().ReverseMap();
            CreateMap<KennelMember, KennelMemberDto>().ReverseMap();
            CreateMap<CreateKennelCommand, Kennel>();
            CreateMap<KennelRoles, KennelRolesDto>().ReverseMap();
            CreateMap<MisManagmentHashers,MisManagmentHashersDto>().ReverseMap();
            CreateMap<CreateKennelRoleCommand, KennelRoles>().ReverseMap();
        }
    }
}
