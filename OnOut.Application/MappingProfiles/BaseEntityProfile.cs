using AutoMapper;
using OnOut.Application.Features.BaseEntity;
using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.MappingProfiles
{
    public class BaseEntityProfile:Profile
    {
        public BaseEntityProfile() 
        {
            CreateMap<BaseEntity, BaseEntityDto>().ReverseMap();
        }
    }
}
