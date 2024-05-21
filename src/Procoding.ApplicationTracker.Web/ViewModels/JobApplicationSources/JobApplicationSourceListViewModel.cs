using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.ViewModels.JobApplicationSources;

public class JobApplicationSourceListViewModel : ViewModelBase
{
    private readonly IJobApplicationSourceService _jobApplicationSourceService;

    public IReadOnlyCollection<JobApplicationSourceDTO> JobApplicationSources { get; set; } = new List<JobApplicationSourceDTO>();

    public JobApplicationSourceListViewModel(IJobApplicationSourceService jobApplicationSourceService)
    {
        _jobApplicationSourceService = jobApplicationSourceService;
    }

    public async Task InitializeViewModel(CancellationToken cancellationToken = default)
    {
        IsLoading = true;
        var response = await _jobApplicationSourceService.GetJobApplicationSourcesAsync(cancellationToken);
        IsLoading = false;

        if (response.IsSuccess)
        {
            JobApplicationSources = response.Value.JobApplicationSources;
        }
    }
}
