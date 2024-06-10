using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Exceptions;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using System.ComponentModel.DataAnnotations;

namespace Procoding.ApplicationTracker.Application.Candidates.Commands.UpdateCandidate;

internal sealed class UpdateCandidateCommandHandler : ICommandHandler<UpdateCandidateCommand, CandidateUpdatedResponseDTO>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IUnitOfWork _unitOfWork;
    readonly UserManager<Candidate> _userManager;

    public UpdateCandidateCommandHandler(ICandidateRepository candidateRepository, IUnitOfWork unitOfWork, UserManager<Candidate> userManager)
    {
        _userManager = userManager;
        _candidateRepository = candidateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CandidateUpdatedResponseDTO>> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = await _candidateRepository.GetCandidateAsync(request.Id, cancellationToken);

        //TODO: use result object
        if (candidate is null)
        {
            throw new CandidateDoesNotExistException("Candidate does not exist");
        }

        var email = new Email(request.Email);

        var result = await candidate.Update(request.Name, request.Surname, email, _userManager);

        if (!result.UpdateResult.Succeeded)
        {
            return new Result<CandidateUpdatedResponseDTO>(new ValidationException(string.Join(",", result.UpdateResult.Errors)));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //TODO: in case of failure
        var candidateDto = new CandidateDTO(candidate.Id,
                                            name: candidate.Name,
                                            surname: candidate.Surname,
                                            email: candidate.Email.Value);

        return new CandidateUpdatedResponseDTO(candidateDto);
    }
}
