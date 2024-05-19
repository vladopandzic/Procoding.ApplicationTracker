using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.ViewModels;

public class JobApplicationSourceListViewModel
{
    private readonly IJobApplicationSourceService _jobApplicationSourceService;

    public IReadOnlyCollection<JobApplicationSourceDTO> JobApplicationSources { get; set; } = new List<JobApplicationSourceDTO>();

    public JobApplicationSourceListViewModel(IJobApplicationSourceService jobApplicationSourceService)
    {
        _jobApplicationSourceService = jobApplicationSourceService;
    }

    public async Task InitializeViewModel(CancellationToken cancellationToken = default)
    {
        var response = await _jobApplicationSourceService.GetJobApplicationSourcesAsync(cancellationToken);

        if (response is not null)
        {
            JobApplicationSources = response.JobApplicationSources;
        }
    }
}
