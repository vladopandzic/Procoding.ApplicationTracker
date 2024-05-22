using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.ViewModels.Candidates;

public class CandidateListViewModel : ViewModelBase
{
    private readonly ICandidateService _candidateService;

    public IReadOnlyCollection<CandidateDTO> Candidates { get; set; } = new List<CandidateDTO>();

    public int TotalNumberOfCandidates { get; set; }
    public CandidateGetListRequestDTO Request { get; set; } = new CandidateGetListRequestDTO();

    public CandidateListViewModel(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }


    public async Task GetCandidates(CancellationToken cancellationToken = default)
    {

        IsLoading = true;
        var response = await _candidateService.GetCandidatesAsync(Request, cancellationToken);
        IsLoading = false;

        if (response.IsSuccess)
        {
            Candidates = response.Value.Candidates;
            TotalNumberOfCandidates = response.Value.TotalCount;
        }
    }
}