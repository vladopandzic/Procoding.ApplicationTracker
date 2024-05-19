namespace Procoding.ApplicationTracker.DTOs.Request.Companies;

public record CompanyUpdateRequestDTO(Guid Id, string Name, string OfficialWebSiteLink);