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
                NeedsProfileCompletion = existing.ProfileCompleted == false,
                HomeCity = existing.HomeCity,
                HomeState = existing.HomeState,
                HomeCountry = existing.HomeCountry
            };
        }

        var profileName = request.Name;
        if (string.IsNullOrWhiteSpace(profileName))
        {
            profileName = request.Email;
        }
        if (string.IsNullOrWhiteSpace(profileName))
        {
            profileName = "New Hasher";
        }

        var newHasher = new Hasher
        {
            Id = Guid.NewGuid(),
            Auth0Subject = request.Subject,
            UserId = request.Subject,
            Email = request.Email ?? string.Empty,
            Name = profileName,
            HashName = profileName,
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