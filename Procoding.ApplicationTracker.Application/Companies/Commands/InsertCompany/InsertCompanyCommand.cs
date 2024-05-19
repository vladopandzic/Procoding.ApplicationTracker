using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Procoding.ApplicationTracker.Application.Companies.Commands.InsertCompany;

public class InsertCompanyCommand : ICommand<CompanyInsertedResponseDTO>
{
    public InsertCompanyCommand(string name, string officialWebSiteLink)
    {
        Name = name;
        OfficialWebSiteLink = officialWebSiteLink;
    }

    public string Name { get; set; }

    public string OfficialWebSiteLink { get; set; }
}
