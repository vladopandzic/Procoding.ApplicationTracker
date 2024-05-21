using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Application.Companies.Commands.InsertCompany;

public sealed class InsertCompanyCommand : ICommand<Result<CompanyInsertedResponseDTO>>
{
    public InsertCompanyCommand(string name, string officialWebSiteLink)
    {
        Name = name;
        OfficialWebSiteLink = officialWebSiteLink;
    }

    public string Name { get; set; }

    public string OfficialWebSiteLink { get; set; }
}
