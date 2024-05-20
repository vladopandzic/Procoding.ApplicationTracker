using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Exceptions;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Application.Candidates.Commands.UpdateCandidate;

internal class UpdateCandidateCommandHandler : IQueryHandler<UpdateCandidateCommand, CandidateUpdatedResponseDTO>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCandidateCommandHandler(ICandidateRepository candidateRepository, IUnitOfWork unitOfWork)
    {
        _candidateRepository = candidateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CandidateUpdatedResponseDTO> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = await _candidateRepository.GetCandidateAsync(request.Id, cancellationToken);

        //TODO: use result object
        if (candidate is null)
        {
            throw new CandidateDoesNotExistException("Candidate does not exist");
        }

        var email = new Email(request.Email);

        candidate.Update(request.Name, request.Surname, email);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //TODO: in case of failure
        var candidateDto = new CandidateDTO(candidate.Id, name: candidate.Name, surname: candidate.Surname, email: candidate.Email.Value);

        return new CandidateUpdatedResponseDTO(candidateDto);
    }
}
