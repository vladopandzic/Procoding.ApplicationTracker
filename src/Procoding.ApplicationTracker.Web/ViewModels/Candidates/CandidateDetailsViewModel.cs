using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.Validators;

namespace Procoding.ApplicationTracker.Web.ViewModels.Candidates;

public class CandidateDetailsViewModel : ViewModelBase
{
    private readonly ICandidateService _candidateService;

    public CandidateDTO? Candidate { get; set; }

    public CandidateValidator Validator { get; }

    public CandidateDetailsViewModel(ICandidateService candidateService,
                                     CandidateValidator validator)
    {
        _candidateService = candidateService;
        Validator = validator;
    }

    public async Task InitializeViewModel(Guid? id, CancellationToken cancellationToken = default)
    {
        if (id is null)
        {
            Candidate = new CandidateDTO(Guid.Empty, "", "", "");
            return;
        }
        IsLoading = true;
        var response = await _candidateService.GetCandidateAsync(id.Value);
        IsLoading = false;

        if (response is not null)
        {
            Candidate = response.Candidate;
        }
    }

    public async Task<bool> IsValidAsync()
    {
        return (await Validator.ValidateAsync(Candidate!)).IsValid;
    }

    public async Task SaveAsync()
    {
        if (Candidate.Id == Guid.Empty)
        {
            await _candidateService.InsertCandidateAsync(
           new CandidateInsertRequestDTO(Candidate!.Name, Candidate.Surname, Candidate.Email));
        }
        else
        {
            await _candidateService.UpdateCandidateAsync(
               new CandidateUpdateRequestDTO(Candidate!.Id, Candidate!.Name, Candidate.Surname, Candidate.Email));
        }

    }
}