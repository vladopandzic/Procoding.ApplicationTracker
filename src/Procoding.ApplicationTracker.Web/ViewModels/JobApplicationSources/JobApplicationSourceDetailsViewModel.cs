using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.JobApplicationSources;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.Validators;
using Procoding.ApplicationTracker.Web.ViewModels.Abstractions;

namespace Procoding.ApplicationTracker.Web.ViewModels.JobApplicationSources;

public class JobApplicationSourceDetailsViewModel : EditViewModelBase
{
    private readonly IJobApplicationSourceService _jobApplicationSourceService;
    private readonly INotificationService _notificationService;

    public JobApplicationSourceDTO? JobApplicationSource { get; set; }

    public JobApplicationSourceValidator Validator { get; }

    public string? PageTitle { get; set; }

    public JobApplicationSourceDetailsViewModel(IJobApplicationSourceService jobApplicationSourceService,
                                               JobApplicationSourceValidator validator,
                                               INotificationService notificationService)
    {
        _jobApplicationSourceService = jobApplicationSourceService;
        Validator = validator;
        _notificationService = notificationService;
    }

    public async Task InitializeViewModel(Guid? id, CancellationToken cancellationToken = default)
    {
        if (id is null)
        {
            JobApplicationSource = new JobApplicationSourceDTO(Guid.Empty, "");
            SetPageTitle();
            return;
        }
        IsLoading = true;
        var response = await _jobApplicationSourceService.GetJobApplicationSourceAsync(id.Value);
        IsLoading = false;

        if (response.IsSuccess)
        {
            JobApplicationSource = response.Value.JobApplicationSource;
        }
        SetPageTitle();
    }

    public async Task<bool> IsValidAsync()
    {
        return (await Validator.ValidateAsync(JobApplicationSource!)).IsValid;
    }

    public async Task SaveAsync()
    {

        if (!(await IsValidAsync()))
        {
            return;
        }

        IsSaving = true;

        if (JobApplicationSource!.Id == Guid.Empty)
        {
            var result = await _jobApplicationSourceService.InsertJobApplicationSourceAsync(
                             new JobApplicationSourceInsertRequestDTO(JobApplicationSource!.Name));

            if (result.IsSuccess)
            {
                JobApplicationSource.Id = result.Value.JobApplicationSource.Id;
            }

            _notificationService.ShowMessageFromResult(result);
        }
        else
        {
            var result = await _jobApplicationSourceService.UpdateJobApplicationSourceAsync(
                            new JobApplicationSourceUpdateRequestDTO(JobApplicationSource!.Id, JobApplicationSource!.Name));

            _notificationService.ShowMessageFromResult(result);
        }

        IsSaving = false;
    }

    private void SetPageTitle()
    {
        PageTitle = JobApplicationSource?.Id == Guid.Empty ? "New job application source" : $"Edit job application source: {JobApplicationSource!.Name}";
    }
}
