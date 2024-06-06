using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.Web.ViewModels.JobApplications;
using Procoding.ApplicationTracker.Web.ViewModels.JobApplicationSources;
using static MudBlazor.Colors;

namespace Procoding.ApplicationTracker.Web.Pages.JobApplication;

[Authorize]
public partial class JobApplicationDetailsPage
{
    [Inject]
    public JobApplicationDetailsViewModel ViewModel { get; set; } = default!;

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

    private string CompaniesToStringFunc(CompanyDTO company)
    {
        return company?.CompanyName ?? string.Empty;
    }

    private string CandidatesToStringFunc(CandidateDTO candidate)
    {
        return candidate != null ? candidate.Name + " " + candidate.Surname : "";
    }

    private string JobApplicationSourcesToStringFunc(JobApplicationSourceDTO jobApplicationSource)
    {
        return jobApplicationSource != null ? jobApplicationSource.Name : "";
    }

    protected async Task<IEnumerable<CompanyDTO>> SearchCompaniesFunc(string value)
    {
        if (value is null)
        {
            return [];
        }
        return ViewModel.Companies.Where(x => x.CompanyName.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }

    protected async Task<IEnumerable<JobApplicationSourceDTO>> SearchJobApplicationSourceFunc(string value)
    {
        if (value is null)
        {
            return [];
        }
        return ViewModel.JobApplicationSources.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }

    protected async Task<IEnumerable<CandidateDTO>> SearchCandidatesFunc(string value)
    {
        if (value is null)
        {
            return [];
        }
        return ViewModel.Candidates.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }

}