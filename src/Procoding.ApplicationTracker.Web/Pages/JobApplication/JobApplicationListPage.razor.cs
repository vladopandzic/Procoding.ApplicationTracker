using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.JobApplications;
using Procoding.ApplicationTracker.Web.ViewModels.JobApplications;

namespace Procoding.ApplicationTracker.Web.Pages.JobApplication;

[Authorize]
public partial class JobApplicationListPage
{
    [Inject]
    public JobApplicationListViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task<GridData<JobApplicationDTO>> LoadGridData(GridState<JobApplicationDTO> state)
    {

        ViewModel.Request = GridStateConverter.ConvertToRequest<JobApplicationGetListRequestDTO, JobApplicationDTO>(state);

        await ViewModel.GetJobApplications();

        GridData<JobApplicationDTO> data = new GridData<JobApplicationDTO>
        {
            Items = ViewModel.JobApplications,
            TotalItems = ViewModel.TotalNumberOfJobApplications
        };
        return data;
    }
}
