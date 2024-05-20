using Microsoft.AspNetCore.Components;
using Procoding.ApplicationTracker.Web.ViewModels.Candidates;

namespace Procoding.ApplicationTracker.Web.Pages.Candidate;

public partial class CandidateListPage
{
    [Inject]
    public CandidateListViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializeViewModel();
        await base.OnInitializedAsync();
    }
}
