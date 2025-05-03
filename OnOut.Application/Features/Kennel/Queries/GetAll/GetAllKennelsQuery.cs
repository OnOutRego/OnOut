using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Kennel.Queries.GetAll
{
    public class GetAllKennelsQuery: IRequest<List<KennelListDto>>
    {
    }
}
