using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Request.JobApplicationSources;
using Procoding.ApplicationTracker.Web.Services;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.Validators;
using Procoding.ApplicationTracker.Web.ViewModels.Abstractions;

namespace Procoding.ApplicationTracker.Web.ViewModels.Candidates;

public class CandidateDetailsViewModel : EditViewModelBase
{
    private readonly ICandidateService _candidateService;
    private readonly INotificationService _notificationService;

    public CandidateDTO? Candidate { get; set; }

    public CandidateValidator Validator { get; }

    public CandidateDetailsViewModel(ICandidateService candidateService,
                                     CandidateValidator validator,
                                     INotificationService notificationService)
    {
        _candidateService = candidateService;
        Validator = validator;
        _notificationService = notificationService;
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

        if (response.IsSuccess)
        {
            Candidate = response.Value.Candidate;
        }
    }

    public async Task<bool> IsValidAsync()
    {
        return (await Validator.ValidateAsync(Candidate!)).IsValid;
    }

    public async Task SaveAsync()
    {
       
        if (!(await IsValidAsync()))
        {
            return;
        }

        IsSaving = true;

        if (Candidate!.Id == Guid.Empty)
        {
            var result = await _candidateService.InsertCandidateAsync(
           new CandidateInsertRequestDTO(Candidate!.Name, Candidate.Surname, Candidate.Email));

            _notificationService.ShowMessageFromResult(result);
        }
        else
        {
            var result = await _candidateService.UpdateCandidateAsync(
               new CandidateUpdateRequestDTO(Candidate!.Id, Candidate!.Name, Candidate.Surname, Candidate.Email));

            _notificationService.ShowMessageFromResult(result);
        }

        IsSaving = false;
    }
}