using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.DTOs.Request.JobApplications;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.Validators;
using Procoding.ApplicationTracker.Web.ViewModels.Abstractions;

namespace Procoding.ApplicationTracker.Web.ViewModels.JobApplications;

public class MyJobApplicationViewModel : EditViewModelBase
{
    private readonly IJobApplicationService _jobApplicationService;
    private readonly IJobApplicationSourceService _jobApplicationSourceService;
    private readonly ICandidateService _candidateService;
    private readonly INotificationService _notificationService;
    private readonly IJobTypeService _jobTypeService;
    private readonly IWorkLocationTypeService _workLocationTypeService;
    private readonly ICompanyService _companyService;

    public JobApplicationDTO? JobApplication { get; set; }

    public List<JobApplicationSourceDTO> JobApplicationSources { get; set; } = new List<JobApplicationSourceDTO>();

    public List<CompanyDTO> Companies { get; set; } = new List<CompanyDTO>();

    public List<JobTypeDTO> JobTypes { get; set; } = new List<JobTypeDTO>();

    public List<WorkLocationTypeDTO> WorkLocationTypes { get; set; } = new List<WorkLocationTypeDTO>();

    //TODO: new valdiator
    public MyNewJobApplicationValidator Validator { get; }

    public CompanyValidator CompanyValidator { get; }

    public bool CreateNewCompanyDialogVisible { get; set; }

    public CompanyDTO NewCompany { get; set; } = default!;

    public string PageTitle { get; set; }

    public MyJobApplicationViewModel(IJobApplicationService jobApplicationService,
                                     ICompanyService companyService,
                                     IJobApplicationSourceService jobApplicationSourceService,
                                     ICandidateService candidateService,
                                     INotificationService notificationService,
                                     IJobTypeService jobTypeService,
                                     IWorkLocationTypeService workLocationTypeService,
                                     MyNewJobApplicationValidator validator,
                                     CompanyValidator companyValidator)
    {
        _companyService = companyService;
        _jobApplicationService = jobApplicationService;
        _jobApplicationSourceService = jobApplicationSourceService;
        _candidateService = candidateService;
        _notificationService = notificationService;
        _jobTypeService = jobTypeService;
        _workLocationTypeService = workLocationTypeService;
        Validator = validator;
        CompanyValidator = companyValidator;
    }

    public async Task InitializeViewModel(Guid? id, CancellationToken cancellationToken = default)
    {
        NewCompany = new CompanyDTO(Guid.Empty, "", "");

        await Task.WhenAll(GetCompanies(cancellationToken),
                           GetJobApplicationSources(cancellationToken),
                           GetJobTypes(cancellationToken),
                           GerWorkLocationTypes(cancellationToken));

        if (id is null)
        {
            var candidate = new CandidateDTO(Guid.Empty, "", "", "", "");
            var jobApplicationSource = new JobApplicationSourceDTO(Guid.Empty, "");
            var company = new CompanyDTO(Guid.Empty, "", "");

            JobApplication = new JobApplicationDTO(id: Guid.Empty,
                                                   candidate: candidate,
                                                   applicationSource: jobApplicationSource,
                                                   company: company,
                                                   jobPositionTitle: "",
                                                   jobAdLink: "",
                                                   jobType: new JobTypeDTO(""),
                                                   workLocationType: new WorkLocationTypeDTO(""),
                                                   description: null);
            SetPageTitle();

            return;
        }
        IsLoading = true;
        var response = await _jobApplicationService.GetJobApplicationAsync(id.Value);
        IsLoading = false;

        if (response is not null)
        {
            JobApplication = response.Value.JobApplication;
        }

        SetPageTitle();
    }

    private void SetPageTitle()
    {
        PageTitle = JobApplication?.Id == Guid.Empty ? "New job application" : $"Job application for {JobApplication?.Company?.CompanyName}";
    }

    public async Task SaveAsync()
    {
        var isJobApplicationValid = (await Validator.ValidateAsync(JobApplication!)).IsValid;

        if (!isJobApplicationValid)
        {
            return;
        }
        IsSaving = true;

        if (JobApplication!.Id == Guid.Empty)
        {
            var result = await _jobApplicationService
                    .InsertJobApplicationAsync(new JobApplicationInsertRequestDTO(JobApplicationSourceId: JobApplication.ApplicationSource!.Id,
                                                                                  CompanyId: JobApplication.Company!.Id,
                                                                                  JobPositionTitle: JobApplication.JobPositionTitle,
                                                                                  JobAdLink: JobApplication.JobAdLink,
                                                                                  JobType: JobApplication.JobType,
                                                                                  WorkLocationType: JobApplication.WorkLocationType,
                                                                                  Description: JobApplication.Description));

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
                                                                                                          JobApplicationSourceId: JobApplication.ApplicationSource!.Id,
                                                                                                          CompanyId: JobApplication.Company!.Id,
                                                                                                          JobPositionTitle: JobApplication.JobPositionTitle,
                                                                                                          JobAdLink: JobApplication.JobAdLink,
                                                                                                          JobType: JobApplication.JobType,
                                                                                                          WorkLocationType: JobApplication.WorkLocationType,
                                                                                                          Description: JobApplication.Description));

            _notificationService.ShowMessageFromResult(result);
        }

        IsSaving = false;
    }

    public async Task SaveNewCompany()
    {
        var isCompanyValid = (await CompanyValidator.ValidateAsync(NewCompany!)).IsValid;
        if (!isCompanyValid)
        {
            return;
        }

        IsSaving = true;

        if (NewCompany!.Id == Guid.Empty)
        {
            var result = await _companyService.InsertCompanyAsync(new CompanyInsertRequestDTO(NewCompany!.CompanyName, NewCompany.OfficialWebSiteLink));

            if (result.IsSuccess)
            {
                Companies.Add(result.Value.Company);
                JobApplication!.Company = result.Value.Company;
                CreateNewCompanyDialogVisible = false;
                NewCompany = new CompanyDTO(Guid.Empty, "", "");
            }
            _notificationService.ShowMessageFromResult(result);
        }


        IsSaving = false;
    }

    private async Task GetCompanies(CancellationToken cancellationToken)
    {
        var companiesResult = await _companyService.GetCompaniesAsync(cancellationToken);
        if (companiesResult.IsSuccess)
        {
            Companies = companiesResult.Value.Companies.ToList();
        }
    }

    private async Task GetJobTypes(CancellationToken cancellationToken)
    {
        var jobTypesResult = await _jobTypeService.GetJobTypesAsync(cancellationToken);
        if (jobTypesResult.IsSuccess)
        {
            JobTypes = jobTypesResult.Value.JobTypes.ToList();
        }
    }

    private async Task GerWorkLocationTypes(CancellationToken cancellationToken)
    {
        var workLocationTypesResult = await _workLocationTypeService.GetWorkLocationTypesAsync(cancellationToken);
        if (workLocationTypesResult.IsSuccess)
        {
            WorkLocationTypes = workLocationTypesResult.Value.WorkLocationTypes.ToList();
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

    public void OpenCreateNewCompanyDialog()
    {
        NewCompany = new CompanyDTO(Guid.Empty, "", "");

        CreateNewCompanyDialogVisible = true;
    }
}
