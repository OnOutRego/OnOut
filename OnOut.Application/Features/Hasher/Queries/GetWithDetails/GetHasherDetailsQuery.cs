using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Hasher.Queries.GetWithDetails
{
    public class GetHasherDetailsQuery: IRequest<HasherDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
