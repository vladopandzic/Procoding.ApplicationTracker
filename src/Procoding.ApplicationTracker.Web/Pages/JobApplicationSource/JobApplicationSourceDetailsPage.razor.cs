using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Procoding.ApplicationTracker.Web.ViewModels.JobApplicationSources;

namespace Procoding.ApplicationTracker.Web.Pages.JobApplicationSource;

[Authorize(Roles = "Employee")]
public partial class JobApplicationSourceDetailsPage
{
    [Inject]
    public JobApplicationSourceDetailsViewModel ViewModel { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    [Parameter]
    public string Id { get; set; }

    protected MudForm? mudForm;

    protected override async Task OnInitializedAsync()
    {
        if (Id == "new")
        {
            await ViewModel.InitializeViewModel(null);
        }
        else
        {
            await ViewModel.InitializeViewModel(Guid.Parse(Id));
        }
        await base.OnInitializedAsync();
    }

    protected async Task OnSubmit()
    {
        await mudForm!.Validate();
        await ViewModel.SaveAsync();
    }
}
