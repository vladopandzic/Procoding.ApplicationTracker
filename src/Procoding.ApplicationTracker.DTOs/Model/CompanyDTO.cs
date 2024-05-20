using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.DTOs.Model;

public class CompanyDTO
{

    public CompanyDTO(Guid id, string name, string officialWebSiteLink)
    {
        Id = id;
        Name = name;
        OfficialWebSiteLink = officialWebSiteLink;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string OfficialWebSiteLink { get; set; }
}
