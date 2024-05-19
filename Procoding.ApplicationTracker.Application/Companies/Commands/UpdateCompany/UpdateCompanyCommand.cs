using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Procoding.ApplicationTracker.Application.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommand : ICommand<CompanyUpdatedResponseDTO>
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
