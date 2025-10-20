using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Kennel.Queries.GetWithDetails
{
    public class GetKennelWithDetailsQuery:IRequest<KennelDto>
    {
        public Guid Id { get; set; }
    }
}
