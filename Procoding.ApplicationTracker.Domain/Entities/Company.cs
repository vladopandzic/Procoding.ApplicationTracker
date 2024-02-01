using Procoding.ApplicationTracker.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Entities;

public sealed class Company
{
    public Company(CompanyName companyName, Link officialWebSite, CompanyFinancialData companyFinancialData)
    {
        CompanyName = companyName;
        OfficialWebSite = officialWebSite;
        CompanyFinancialData = companyFinancialData;
    }

    public Company(CompanyName companyName, Link officialWebSite)
    {
        CompanyName = companyName;
        OfficialWebSite = officialWebSite;
    }

    public CompanyName CompanyName { get; }

    public Link OfficialWebSite { get; }

    public CompanyFinancialData CompanyFinancialData { get; } = CompanyFinancialData.None;
}
