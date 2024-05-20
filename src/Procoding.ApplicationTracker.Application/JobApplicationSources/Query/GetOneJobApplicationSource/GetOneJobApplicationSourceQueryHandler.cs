﻿using MapsterMapper;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Query.GetOneJobApplicationSource;

internal class GetOneJobApplicationSourceQueryHandler : IQueryHandler<GetOneJobApplicationSourceQuery, JobApplicationSourceResponseDTO>
{
    private readonly IMapper _mapper;
    private readonly IJobApplicationSourceRepository _jobApplicationSourceRepository;

    public GetOneJobApplicationSourceQueryHandler(IMapper mapper, IJobApplicationSourceRepository jobApplicationSourceRepository)
    {
        _mapper = mapper;
        _jobApplicationSourceRepository = jobApplicationSourceRepository;
    }

    public async Task<JobApplicationSourceResponseDTO> Handle(GetOneJobApplicationSourceQuery request, CancellationToken cancellationToken)
    {
        var jobApplicationSource = await _jobApplicationSourceRepository.GetJobApplicationSourceAsync(request.Id, cancellationToken);

        //TODO: result object?
        if (jobApplicationSource is null)
            throw new Domain.Exceptions.JobApplicationSourceDoesNotExistException();

        var jobApplicationSourceDto = _mapper.Map<JobApplicationSourceDTO>(jobApplicationSource);

        return new JobApplicationSourceResponseDTO(jobApplicationSourceDto);
    }
}