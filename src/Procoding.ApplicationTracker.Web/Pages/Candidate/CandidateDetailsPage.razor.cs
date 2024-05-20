using Microsoft.AspNetCore.Components;
using MudBlazor;
using Procoding.ApplicationTracker.Web.ViewModels.Candidates;
using Procoding.ApplicationTracker.Web.ViewModels.Companies;

namespace Procoding.ApplicationTracker.Web.Pages.Candidate;

public partial class CandidateDetailsPage
{
    [Inject]
    public CandidateDetailsViewModel ViewModel { get; set; } = default!;

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
