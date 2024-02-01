using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Domain.Entities;

public sealed class CompanyFinancialData
{
    public static CompanyFinancialData None { get; } = new CompanyFinancialData(AverageGrossSalary.None, 0);

    public CompanyFinancialData(AverageGrossSalary averageGrossSalary, int companyId)
    {
        AverageGrossSalary = averageGrossSalary;
        CompanyId = companyId;
    }

    public AverageGrossSalary AverageGrossSalary { get; set; }

    public int CompanyId { get; set; }
}
