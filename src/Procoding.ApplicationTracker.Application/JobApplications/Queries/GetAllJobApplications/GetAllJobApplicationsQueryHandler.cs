using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;

namespace Procoding.ApplicationTracker.Application.JobApplications.Queries.GetAllJobApplications;

internal class GetAllJobApplicationsQueryHandler : IQueryHandler<GetAllJobApplicationsQuery, JobApplicationListResponseDTO>
{
    private readonly IJobApplicationRepository _jobApplicationRepository;

    public GetAllJobApplicationsQueryHandler(IJobApplicationRepository jobApplicationRepository)
    {
        _jobApplicationRepository = jobApplicationRepository;
    }

    public async Task<JobApplicationListResponseDTO> Handle(GetAllJobApplicationsQuery request, CancellationToken cancellationToken)
    {
        var specification = new Specifications.JobApplicationGetListSpecification(request.PageNumber, request.PageSize, request.Filters, request.Sort);

        var jobApplications = await _jobApplicationRepository.GetJobApplicationsAsync(specification, cancellationToken);

        var count = await _jobApplicationRepository.CountAsync(specification, cancellationToken);


        var jobApplicationsDto = jobApplications.Select(x =>
        {
            var candidateDto = new CandidateDTO(x.Candidate.Id, x.Candidate.Name, x.Candidate.Surname, x.Candidate.Email.Value);
            var jobApplicationSourceDto = new JobApplicationSourceDTO(x.ApplicationSource.Id, x.ApplicationSource.Name);
            var companyDto = new CompanyDTO(x.Company.Id, x.Company.CompanyName.Value, x.Company.OfficialWebSiteLink.Value);

            return new JobApplicationDTO(x.Id, candidateDto, jobApplicationSourceDto, companyDto);

        }).ToList();

        return new JobApplicationListResponseDTO(jobApplicationsDto.AsReadOnly(), count);
    }
}
