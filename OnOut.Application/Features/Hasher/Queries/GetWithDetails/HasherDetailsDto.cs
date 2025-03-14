using OnOut.Application.Features.Hasher.Queries.GetAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Hasher.Queries.GetWithDetails
{
    public class HasherDetailsDto: HasherListDto
    {
        public string Email { get; set; }
        public string HomeCity { get; set; }
        public string HomeState { get; set; }
        public string HomeCountry { get; set; }
        public DietaryChoiceDto DietaryChoice { get; set; }
    }
}
