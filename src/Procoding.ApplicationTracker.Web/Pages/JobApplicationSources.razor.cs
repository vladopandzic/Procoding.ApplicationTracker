
using Microsoft.AspNetCore.Components;

namespace Procoding.ApplicationTracker.Web.Pages;

public partial class JobApplicationSources
{
    [Inject]
    public ViewModels.JobApplicationSourceListViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializeViewModel();
        await base.OnInitializedAsync();
    }
}
