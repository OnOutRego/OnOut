using AutoMapper;
using OnOut.Application.Features.Hasher.Queries.GetWithDetails;
using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.MappingProfiles
{
    public class DietaryChoiceProfile: Profile
    {
        public DietaryChoiceProfile()
        {
            CreateMap<DietaryChoice,DietaryChoiceDto>().ReverseMap();
        }
    }
}
