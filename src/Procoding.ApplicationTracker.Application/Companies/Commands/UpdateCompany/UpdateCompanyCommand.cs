using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Application.Companies.Commands.UpdateCompany;

public sealed class UpdateCompanyCommand : ICommand<Result<CompanyUpdatedResponseDTO>>
{
    public UpdateCompanyCommand(Guid id, string companyName, string officialWebsiteLink)
    {
        Id = id;
        CompanyName = companyName;
        OfficialWebsiteLink = officialWebsiteLink;
    }

    public Guid Id { get; set; }

    public string CompanyName { get; set; }

    public string OfficialWebsiteLink { get; set; }
}
