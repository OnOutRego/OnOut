using System;
using System.Collections.Generic;
using MediatR;

namespace OnOut.Application.Features.MismanagmentHasher.Queries.GetByHasher
{
    public class GetByHasherQuery : IRequest<List<MismanagementHasherDto>>
    {
        public Guid HasherId { get; set; }
    }
}