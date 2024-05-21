using FluentValidation;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;

namespace Procoding.ApplicationTracker.Api.Validation;

public class CandidateInsertRequestDTOValidator : AbstractValidator<CandidateInsertRequestDTO>
{
    public CandidateInsertRequestDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Surname).NotEmpty();

        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
public class CandidateUpdateRequestDTOValidator : AbstractValidator<CandidateUpdateRequestDTO>
{
    public CandidateUpdateRequestDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Surname).NotEmpty();

        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
