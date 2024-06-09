using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Request.JobApplications;

/// <summary>
/// Request for inserting job application
/// </summary>
/// <param name="JobApplicationSourceId"></param>
/// <param name="CompanyId"></param>
/// <param name="JobPositionTitle"></param>
/// <param name="JobAdLink"></param>
/// <param name="JobType"></param>
/// <param name="WorkLocationType"></param>
/// <param name="Description"></param>
public record JobApplicationInsertRequestDTO(Guid JobApplicationSourceId,
                                             Guid CompanyId,
                                             string JobPositionTitle,
                                             string JobAdLink,
                                             JobTypeDTO JobType,
                                             WorkLocationTypeDTO WorkLocationType,
                                             string? Description);