using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Application.Candidates.Commands.SignupCandidate;

internal class SignupCandidateCommandHandler : ICommandHandler<SignupCandidateCommand, CandidateSignupResponseDTO>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IPasswordHasher<Candidate> _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    //readonly IRefreshTokenRepository _refreshTokenRepository;
    //private readonly UserManager<Candidate> _userManager;
    //private readonly TimeProvider _timeProvider;
    //private readonly IJwtTokenCreator<Candidate> _jwtTokenCreator;
    //private readonly JwtTokenOptions<Candidate> _jwtTokenOptions;
    //private readonly IUnitOfWork _unitOfWork;

    public SignupCandidateCommandHandler(ICandidateRepository candidateRepository, IPasswordHasher<Candidate> passwordHasher, IUnitOfWork unitOfWork)
    {
        _candidateRepository = candidateRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CandidateSignupResponseDTO>> Handle(SignupCandidateCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var email = new Email(request.Email);


        var employee = Candidate.Create(id, name: request.Name, surname: request.Surname, email: email, password: request.Password, _passwordHasher);

        //TODO: prevent duplicates
        var result = await _candidateRepository.InsertAsync(employee, request.Password, cancellationToken);

        if (!result.Succeeded)
        {
            return new Result<CandidateSignupResponseDTO>(new ValidationException(result.Errors.Select(x => new FluentValidation.Results.ValidationFailure("",
                                                                                                                                                           x.Description))));
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //TODO: in case of failure


        return new CandidateSignupResponseDTO(true);
    }
}
