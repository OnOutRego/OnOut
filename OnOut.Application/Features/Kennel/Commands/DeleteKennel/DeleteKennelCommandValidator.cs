using FluentValidation;
using OnOut.Application.Contracts;

namespace OnOut.Application.Features.Kennel.Commands.DeleteKennel
{
    public class DeleteKennelCommandValidator : AbstractValidator<DeleteKennelCommand>
    {
        public DeleteKennelCommandValidator(IKennelRepository kennelRepository)
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Kennel ID is required.")
                .MustAsync(async (id, cancellation) => await kennelRepository.ExistsById(id))
                .WithMessage("The specified kennel does not exist.");
        }
    }
}