using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.Validators;
using Procoding.ApplicationTracker.DTOs.Request.JobApplicationSources;

namespace Procoding.ApplicationTracker.Web.ViewModels.JobApplicationSources;

public class JobApplicationSourceDetailsViewModel : ViewModelBase
{
    private readonly IJobApplicationSourceService _jobApplicationSourceService;

    public JobApplicationSourceDTO? JobApplicationSource { get; set; }

    public JobApplicationSourceValidator Validator { get; }

    public JobApplicationSourceDetailsViewModel(IJobApplicationSourceService jobApplicationSourceService,
                                               JobApplicationSourceValidator validator)
    {
        _jobApplicationSourceService = jobApplicationSourceService;
        Validator = validator;
    }

    public async Task InitializeViewModel(Guid? id, CancellationToken cancellationToken = default)
    {
        if (id is null) {
            JobApplicationSource = new JobApplicationSourceDTO(Guid.Empty, "");
            return;
        }
        IsLoading = true;
        var response = await _jobApplicationSourceService.GetJobApplicationSourceAsync(id.Value);
        IsLoading = false;

        if (response is not null)
        {
            JobApplicationSource = response.JobApplicationSource;
        }
    }

    public async Task<bool> IsValidAsync()
    {
        return (await Validator.ValidateAsync(JobApplicationSource!)).IsValid;
    }

    public async Task SaveAsync()
    {
        if (JobApplicationSource.Id == Guid.Empty)
        {
            await _jobApplicationSourceService.InsertJobApplicationSourceAsync(
           new JobApplicationSourceInsertRequestDTO(JobApplicationSource!.Name));
        }
        else {
            await _jobApplicationSourceService.UpdateJobApplicationSourceAsync(
               new JobApplicationSourceUpdateRequestDTO(JobApplicationSource!.Id, JobApplicationSource!.Name));
        }
       
    }
}
