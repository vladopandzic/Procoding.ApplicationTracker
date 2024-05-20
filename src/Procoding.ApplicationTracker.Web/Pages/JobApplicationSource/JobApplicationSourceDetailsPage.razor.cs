using Microsoft.AspNetCore.Components;
using Procoding.ApplicationTracker.Web.ViewModels.JobApplicationSources;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace Procoding.ApplicationTracker.Web.Pages.JobApplicationSource;

public partial class JobApplicationSourceDetailsPage
{
    [Inject]
    public JobApplicationSourceDetailsViewModel ViewModel { get; set; } = default!;

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

        var isValid = await ViewModel.IsValidAsync();

        if (isValid)
        {
            await ViewModel.SaveAsync();
        }

    }
}
