using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using OnOut.Application.Features.KennelMember.Commands.UpdateKennelMember;

public class UpdateKennelMemberCommandHandler : IRequestHandler<UpdateKennelMemberCommand, Unit>
{
    private readonly IValidator<UpdateKennelMemberCommand> _validator;
    private readonly IKennelMemberRepository _kennelMemberRepository;
    private readonly IAppLogger<UpdateKennelMemberCommandHandler> _logger;

    public UpdateKennelMemberCommandHandler(
        IValidator<UpdateKennelMemberCommand> validator,
        IKennelMemberRepository kennelMemberRepository,
        IAppLogger<UpdateKennelMemberCommandHandler> logger)
    {
        _validator = validator;
        _kennelMemberRepository = kennelMemberRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateKennelMemberCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for UpdateKennelMemberCommand: {Errors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var kennelMember = await _kennelMemberRepository.GetByIdAsync(request.KennelMemberId);

        kennelMember.RoleId = request.RoleId;
        await _kennelMemberRepository.UpdateAsync(kennelMember);

        _logger.LogInformation("KennelMember with Id {Id} updated successfully.", request.KennelMemberId);

        return Unit.Value;
    }
}