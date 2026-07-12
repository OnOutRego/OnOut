using AutoMapper;
using FluentValidation;
using MediatR;
using OnOut.Application.Contracts;
using OnOut.Application.Contracts.Logging;
using OnOut.Application.Exceptions;
using OnOut.Domain;
public class EnsureHasherFromAuth0CommandHandler : IRequestHandler<EnsureHasherFromAuth0Command, EnsureHasherFromAuth0Result>
{
    private readonly IHasherRepository _hasherRepository;

    public EnsureHasherFromAuth0CommandHandler(IHasherRepository hasherRepository)
    {
        _hasherRepository = hasherRepository;
    }

    public async Task<EnsureHasherFromAuth0Result> Handle(EnsureHasherFromAuth0Command request, CancellationToken cancellationToken)
    {
        var existing = await _hasherRepository.GetByAuth0SubjectAsync(request.Subject);

        if (existing != null)
        {
            return new EnsureHasherFromAuth0Result
            {
                HasherId = existing.Id,
                IsNewUser = false,
                NeedsProfileCompletion = existing.ProfileCompleted == false
            };
        }

        var newHasher = new Hasher
        {
            Id = Guid.NewGuid(),
            Auth0Subject = request.Subject,
            UserId = request.Subject,
            Email = request.Email ?? string.Empty,
            HashName = request.Name ?? request.Email ?? "New Hasher",
            ProfileCompleted = false
        };

        await _hasherRepository.CreateAsync(newHasher);

        return new EnsureHasherFromAuth0Result
        {
            HasherId = newHasher.Id,
            IsNewUser = true,
            NeedsProfileCompletion = true
        };
    }
}