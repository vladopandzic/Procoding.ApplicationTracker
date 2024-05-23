using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.DTOs.Model;

public class CompanyDTO
{

    public CompanyDTO(Guid id, string companyName, string officialWebSiteLink)
    {
        Id = id;
        CompanyName = companyName;
        OfficialWebSiteLink = officialWebSiteLink;
    }

    public Guid Id { get; set; }

    public string CompanyName { get; set; }

    public string OfficialWebSiteLink { get; set; }
}
