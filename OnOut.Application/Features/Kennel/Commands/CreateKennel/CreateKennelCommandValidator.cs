using FluentValidation;
using OnOut.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Application.Features.Kennel.Commands.CreateKennel
{
    public class CreateKennelCommandValidator:AbstractValidator<CreateKennelCommand>
    {
        private readonly IKennelRepository _kennelRepository;

        public CreateKennelCommandValidator(IKennelRepository kennelRepository)
        {
            this._kennelRepository = kennelRepository;
        }
    }
}
