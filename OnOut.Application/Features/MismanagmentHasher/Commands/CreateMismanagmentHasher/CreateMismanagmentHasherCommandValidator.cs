using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using OnOut.Application.Contracts;

namespace OnOut.Application.Features.MismanagmentHasher.Commands.CreateMismanagmentHasher
{
    internal class CreateMismanagmentHasherCommandValidator : AbstractValidator<CreateMismanagmentHasherCommand>
    {
        private readonly IHasherRepository _hasherRepository;
        private readonly IKennelRepository _kennelRepository;

        public CreateMismanagmentHasherCommandValidator(IHasherRepository hasherRepository, 
                                                        IKennelRepository kennelRepository)
        {
            _hasherRepository = hasherRepository;
            _kennelRepository = kennelRepository;

            RuleFor(x => x.HasherId)
                .MustAsync(async (id, cancellation) => await _hasherRepository.ExistsById(id))
                .WithMessage("Hasher with the specified Id does not exist.");

            RuleFor(x => x.KennelId)
                .MustAsync(async (id, cancellation) => await _kennelRepository.ExistsById(id))
                .WithMessage("Kennel with the specified Id does not exist.");

        
            RuleFor(x => x.HasherId)
                .NotEmpty().WithMessage("HasherId is required.");

            RuleFor(x => x.KennelId)
                .NotEmpty().WithMessage("KennelId is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
           
        }
    }
}
