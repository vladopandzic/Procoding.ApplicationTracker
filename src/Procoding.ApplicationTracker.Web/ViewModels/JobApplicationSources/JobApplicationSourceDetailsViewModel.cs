using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.ViewModels.JobApplicationSources;

public class JobApplicationSourceDetailsViewModel
{
    private readonly IJobApplicationSourceService _jobApplicationSourceService;

    public JobApplicationSourceDTO? JobApplicationSource { get; set; }

    public JobApplicationSourceDetailsViewModel(IJobApplicationSourceService jobApplicationSourceService)
    {
        _jobApplicationSourceService = jobApplicationSourceService;
    }

    public async Task InitializeViewModel(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _jobApplicationSourceService.GetJobApplicationSourceAsync(id);

        if (response is not null)
        {
            JobApplicationSource = response.JobApplicationSource;
        }
    }
}
