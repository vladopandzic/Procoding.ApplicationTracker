using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.JobApplications;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.ViewModels.JobApplications;

public class JobApplicationListViewModel : ViewModelBase
{
    private readonly IJobApplicationService _jobApplicationService;

    public IReadOnlyCollection<JobApplicationDTO> JobApplications { get; set; } = new List<JobApplicationDTO>();

    public int TotalNumberOfJobApplications { get; set; }
    public JobApplicationGetListRequestDTO Request { get; set; } = new JobApplicationGetListRequestDTO();

    public JobApplicationListViewModel(IJobApplicationService jobApplicationService)
    {
        _jobApplicationService = jobApplicationService;
    }


    public async Task GetJobApplications(CancellationToken cancellationToken = default)
    {

        IsLoading = true;
        var response = await _jobApplicationService.GetJobApplicationsAsync(Request, cancellationToken);
        IsLoading = false;

        if (response.IsSuccess)
        {
            JobApplications = response.Value.JobApplications;
            TotalNumberOfJobApplications = response.Value.TotalCount;
        }
    }
}
