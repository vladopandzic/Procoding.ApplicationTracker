using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.DTOs.Model;

public class CompanyDTO
{
    public CompanyDTO(string name, string officialWebSiteLink)
    {
        Name = name;
        OfficialWebSiteLink = officialWebSiteLink;
    }

    public string Name { get; }

    public string OfficialWebSiteLink { get; }
}
