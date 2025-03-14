using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Hasher.Queries.GetAll
{
    public class GetAllHasherQuery: IRequest<List<HasherListDto>>
    {
    }
}
