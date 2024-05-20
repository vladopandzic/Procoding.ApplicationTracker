using Microsoft.AspNetCore.Components;
using Procoding.ApplicationTracker.Web.ViewModels.JobApplicationSources;

namespace Procoding.ApplicationTracker.Web.Pages;

public partial class JobApplicationSourceDetailsPage
{
    [Inject]
    public JobApplicationSourceDetailsViewModel ViewModel { get; set; } = default!;

    [Parameter]
    public string Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializeViewModel(Guid.Parse(Id));
        await base.OnInitializedAsync();
    }
}
