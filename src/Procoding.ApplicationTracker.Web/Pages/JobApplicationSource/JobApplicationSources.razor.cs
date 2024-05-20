
using Microsoft.AspNetCore.Components;
using Procoding.ApplicationTracker.Web.ViewModels.JobApplicationSources;

namespace Procoding.ApplicationTracker.Web.Pages.JobApplicationSource;

public partial class JobApplicationSources
{
    [Inject]
    public JobApplicationSourceListViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializeViewModel();
        await base.OnInitializedAsync();
    }
}
