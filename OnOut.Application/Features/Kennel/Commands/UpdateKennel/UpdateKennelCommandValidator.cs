using FluentValidation;
using OnOut.Application.Contracts;

namespace OnOut.Application.Features.Kennel.Commands.UpdateKennel
{
    public class UpdateKennelCommandValidator : AbstractValidator<UpdateKennelCommand>
    {
        public UpdateKennelCommandValidator(IKennelRepository kennelRepository)
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Kennel ID is required.")
                .MustAsync(async (kennelId, cancellation) => 
                    await kennelRepository.ExistsById(kennelId))
                .WithMessage("The specified kennel does not exist.");
            
        }
    }
}