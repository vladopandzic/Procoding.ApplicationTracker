using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.ViewModels.Candidates;

public class CandidateListViewModel : ViewModelBase
{
    private readonly ICandidateService _candidateService;

    public IReadOnlyCollection<CandidateDTO> Candidates { get; set; } = new List<CandidateDTO>();

    public CandidateListViewModel(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }

    public async Task InitializeViewModel(CancellationToken cancellationToken = default)
    {
        IsLoading = true;
        var response = await _candidateService.GetCandidatesAsync(cancellationToken);
        IsLoading = false;

        if (response is not null)
        {
            Candidates = response.Candidates;
        }
    }
}