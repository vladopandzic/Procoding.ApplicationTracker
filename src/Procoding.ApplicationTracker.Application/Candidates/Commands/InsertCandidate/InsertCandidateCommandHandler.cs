using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Application.Candidates.Commands.InsertCandidate;

internal sealed class InsertCandidateCommandHandler : ICommandHandler<InsertCandidateCommand, CandidateInsertedResponseDTO>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InsertCandidateCommandHandler(ICandidateRepository candidateRepository, IUnitOfWork unitOfWork)
    {
        _candidateRepository = candidateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CandidateInsertedResponseDTO>> Handle(InsertCandidateCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var email = new Email(request.Email);
        var candidate = Candidate.Create(id, name: request.Name, surname: request.Surname, email: email);

        await _candidateRepository.InsertAsync(candidate, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //TODO: in case of failure
        var candidateDto = new CandidateDTO(id: candidate.Id, name: candidate.Name, surname: candidate.Surname, email: candidate.Email.Value);

        return new CandidateInsertedResponseDTO(candidateDto);
    }
}
