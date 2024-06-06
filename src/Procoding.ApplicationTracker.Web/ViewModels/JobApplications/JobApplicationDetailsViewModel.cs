using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Request.JobApplications;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.Validators;
using Procoding.ApplicationTracker.Web.ViewModels.Abstractions;

namespace Procoding.ApplicationTracker.Web.ViewModels.JobApplications;

public class JobApplicationDetailsViewModel : EditViewModelBase
{
    private readonly IJobApplicationService _jobApplicationService;
    private readonly IJobApplicationSourceService _jobApplicationSourceService;
    private readonly ICandidateService _candidateService;
    private readonly INotificationService _notificationService;
    private readonly ICompanyService _companyService;

    public JobApplicationDTO? JobApplication { get; set; }

    public List<JobApplicationSourceDTO> JobApplicationSources { get; set; } = new List<JobApplicationSourceDTO>();

    public List<CompanyDTO> Companies { get; set; } = new List<CompanyDTO>();

    public List<CandidateDTO> Candidates { get; set; } = new List<CandidateDTO>();

    public JobApplicationValidator Validator { get; }

    public JobApplicationDetailsViewModel(IJobApplicationService jobApplicationService,
                                          ICompanyService companyService,
                                          IJobApplicationSourceService jobApplicationSourceService,
                                          ICandidateService candidateService,
                                          INotificationService notificationService,
                                          JobApplicationValidator validator)
    {
        _companyService = companyService;
        _jobApplicationService = jobApplicationService;
        _jobApplicationSourceService = jobApplicationSourceService;
        _candidateService = candidateService;
        _notificationService = notificationService;
        Validator = validator;
    }

    public async Task InitializeViewModel(Guid? id, CancellationToken cancellationToken = default)
    {
        await Task.WhenAll(GetCompanies(cancellationToken), GetCandidates(cancellationToken), GetJobApplicationSources(cancellationToken));

        if (id is null)
        {
            var candidate = new CandidateDTO(Guid.NewGuid(), "", "", "", "");
            var jobApplicationSource = new JobApplicationSourceDTO(Guid.NewGuid(), "");
            var company = new CompanyDTO(Guid.NewGuid(), "", "");

            JobApplication = new JobApplicationDTO(Guid.Empty, candidate, jobApplicationSource, company);

            return;
        }
        IsLoading = true;
        var response = await _jobApplicationService.GetJobApplicationAsync(id.Value);
        IsLoading = false;

        if (response is not null)
        {
            JobApplication = response.Value.JobApplication;
        }
    }

    private async Task GetCompanies(CancellationToken cancellationToken)
    {
        var companiesResult = await _companyService.GetCompaniesAsync(cancellationToken);
        if (companiesResult.IsSuccess)
        {
            Companies = companiesResult.Value.Companies.ToList();
        }
    }

    private async Task GetCandidates(CancellationToken cancellationToken)
    {
        var candidatesResult = await _candidateService.GetCandidatesAsync(new EmployeeGetListRequestDTO(), cancellationToken);
        if (candidatesResult.IsSuccess)
        {
            Candidates = candidatesResult.Value.Candidates.ToList();
        }
    }

    private async Task GetJobApplicationSources(CancellationToken cancellationToken)
    {
        var jobApplicationSourceResult = await _jobApplicationSourceService.GetJobApplicationSourcesAsync(cancellationToken);
        if (jobApplicationSourceResult.IsSuccess)
        {
            JobApplicationSources = jobApplicationSourceResult.Value.JobApplicationSources.ToList();
        }
    }

    public async Task<bool> IsValidAsync()
    {
        return (await Validator.ValidateAsync(JobApplication!)).IsValid;
    }

    public async Task SaveAsync()
    {
        if (!(await IsValidAsync()))
        {
            return;
        }

        IsSaving = true;

        if (JobApplication!.Id == Guid.Empty)
        {
            var result = await _jobApplicationService
                    .InsertJobApplicationAsync(new JobApplicationInsertRequestDTO(CandidateId: JobApplication.Candidate!.Id,
                                                                                  JobApplicationSourceId: JobApplication.ApplicationSource!.Id,
                                                                                  CompanyId: JobApplication.Company!.Id));

            if (result.IsSuccess)
            {
                JobApplication.Id = result.Value.JobApplication.Id;
            }
            _notificationService.ShowMessageFromResult(result);
        }
        else
        {
            var result =
                await _jobApplicationService.UpdateJobApplicationAsync(new JobApplicationUpdateRequestDTO(Id: JobApplication.Id,
                                                                                                          CandidateId: JobApplication.Candidate!.Id,
                                                                                                          JobApplicationSourceId: JobApplication.ApplicationSource!.Id,
                                                                                                          CompanyId: JobApplication.Company!.Id));

            _notificationService.ShowMessageFromResult(result);
        }

        IsSaving = false;
    }
}