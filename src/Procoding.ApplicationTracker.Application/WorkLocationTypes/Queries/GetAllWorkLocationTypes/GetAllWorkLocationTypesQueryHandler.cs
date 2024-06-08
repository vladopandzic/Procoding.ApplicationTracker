using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Application.JobTypes.Queries.GetAllJobTypes;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.JobTypes;
using Procoding.ApplicationTracker.DTOs.Response.WorkLocationTypes;

namespace Procoding.ApplicationTracker.Application.WorkLocationTypes.Queries.GetAllJobWorkTypes;

internal sealed class GetAllWorkLocationTypesQueryHandler : IQueryHandler<GetAllWorkLocationTypesQuery, WorkLocationTypeListResponse>
{
    private readonly IWorkLocationTypeRepository _workLocationTypeRepository;

    public GetAllWorkLocationTypesQueryHandler(IWorkLocationTypeRepository workLocationTypeRepository)
    {
        _workLocationTypeRepository = workLocationTypeRepository;
    }

    public async Task<WorkLocationTypeListResponse> Handle(GetAllWorkLocationTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await _workLocationTypeRepository.GetAllWorkLocationTypesAsync(cancellationToken);

        var workLocations = result.Select(x => new WorkLocationTypeDTO(x.Value)).ToList();

        return new WorkLocationTypeListResponse(workLocations, workLocations.Count());
    }
}
