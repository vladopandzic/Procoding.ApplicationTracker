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
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
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
    private readonly IPasswordHasher<Candidate> _passwordHasher;

    public InsertCandidateCommandHandler(ICandidateRepository candidateRepository, IUnitOfWork unitOfWork, IPasswordHasher<Candidate> passwordHasher)
    {
        _candidateRepository = candidateRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<CandidateInsertedResponseDTO>> Handle(InsertCandidateCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var email = new Email(request.Email);

        var candidate = Candidate.Create(id, name: request.Name, surname: request.Surname, email: email, password: request.Password, _passwordHasher);

        var result = await _candidateRepository.InsertAsync(candidate, request.Password, cancellationToken);

        if (!result.Succeeded)
        {
            return new Result<CandidateInsertedResponseDTO>(new ValidationException(result.Errors.Select(x => new FluentValidation.Results.ValidationFailure("",
                                                                                                                                                            x.Description))));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //TODO: in case of failure
        var candidateDto = new CandidateDTO(id: candidate.Id,
                                            name: candidate.Name,
                                            surname: candidate.Surname,
                                            email: candidate.Email.Value,
                                            password: candidate.PasswordHash);

        return new CandidateInsertedResponseDTO(candidateDto);
    }
}
