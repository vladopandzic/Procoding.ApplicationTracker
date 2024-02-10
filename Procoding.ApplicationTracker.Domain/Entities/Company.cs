using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Common;
using Procoding.ApplicationTracker.Domain.Events;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using System.Xml.Linq;

namespace Procoding.ApplicationTracker.Domain.Entities;

/// <summary>
/// Represents company the candidate applies for.
/// </summary>
public sealed class Company : AggregateRoot, ISoftDeletableEntity, IAuditableEntity
{
    private readonly List<CompanyAverageGrossSalary> _companyAverageGrossSalaries = new();

    /// <summary>
    /// Creates new instance of the <see cref="Company"/>. Required by EF Core.
    /// </summary>
#pragma warning disable CS8618
    private Company()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// Creates new instance of the <see cref="Company"/>.
    /// </summary>
    /// <param name="id">Id of the company</param>
    /// <param name="companyName">Company name.</param>
    /// <param name="officialWebSiteLink">Official website. Must be valid link.</param>
    /// <param name="averageGrossSalaries">List of salaries for each <see cref="CompanyAverageGrossSalary.Year"/></param>
    public Company(Guid id, CompanyName companyName, Link officialWebSiteLink, ICollection<CompanyAverageGrossSalary> averageGrossSalaries) : base(id)
    {
        CompanyName = companyName;
        OfficialWebSiteLink = officialWebSiteLink;
    }

    /// <summary>
    /// Creates new instance of the <see cref="Company"/>.
    /// </summary>
    /// <param name="id">Id of the company</param>
    /// <param name="companyName">Company name.</param>
    /// <param name="officialWebSiteLink">Official website. Must be valid link.</param>
    private Company(Guid id, CompanyName companyName, Link officialWebSiteLink) : base(id)
    {
        ValidateParameters(companyName, officialWebSiteLink);
        CompanyName = companyName;
        OfficialWebSiteLink = officialWebSiteLink;
    }

    private static void ValidateParameters(CompanyName companyName, Link officialWebSiteLink)
    {
        if (companyName is null)
        {
            throw new ArgumentNullException(nameof(companyName));
        }
        if (officialWebSiteLink is null)
        {
            throw new ArgumentNullException(nameof(officialWebSiteLink));
        }
    }

    /// <summary>
    /// Company name. Must be at most <see cref="CompanyName.MaxLengthForName"/> long.
    /// </summary>
    public CompanyName CompanyName { get; private set; }

    /// <summary>
    /// Official website. Must be valid link. Must be at most <see cref="Link.MaxLengthForValue"/> long."/>
    /// </summary>
    public Link OfficialWebSiteLink { get; private set; }

    /// <summary>
    /// List of average gross salaries  for each <see cref="CompanyAverageGrossSalary.Year"/>.
    /// </summary>
    public IReadOnlyList<CompanyAverageGrossSalary> CompanyAverageGrossSalaries => _companyAverageGrossSalaries;

    /// <inheritdoc/>
    public DateTime? DeletedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime CreatedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime ModifiedOnUtc { get; }

    /// <summary>
    /// Creates new company
    /// </summary>
    /// <param name="companyName"></param>
    /// <param name="officialWebSiteLink"></param>
    /// <returns></returns>
    public static Company Create(CompanyName companyName, Link officialWebSiteLink)
    {
        var company = new Company(id: Guid.NewGuid(),
                                  companyName: companyName,
                                  officialWebSiteLink: officialWebSiteLink);

        company.AddDomainEvent(new CompanyCreatedDomainEvent(company.Id, company.CompanyName));
        return company;
    }

    /// <summary>
    /// Updates company with basic info
    /// </summary>
    /// <param name="companyName"></param>
    /// <param name="officialWebSiteLink"></param>
    /// <returns></returns>
    public Company Update(CompanyName companyName, Link officialWebSiteLink)
    {
        CompanyName = companyName;
        OfficialWebSiteLink = officialWebSiteLink;
        ValidateParameters(companyName, officialWebSiteLink);


        this.AddDomainEvent(new CompanyUpdatedDomainEvent(this.Id, this.CompanyName, this.OfficialWebSiteLink));

        return this;
    }

    public void AddGrossSalaryForYear(decimal grossSalary, int year, Currency currency)
    {
        //INT check if already exists for year
        var grossSalaryForYear = CompanyAverageGrossSalary.Create(company: this,
                                                                 id: Guid.NewGuid(),
                                                                 grossSalary: grossSalary,
                                                                 year: year,
                                                                 currency: currency);


        _companyAverageGrossSalaries.Add(grossSalaryForYear);

        this.AddDomainEvent(new GrossSalaryForCompanyAddedDomainEvent(grossSalaryForYear.Id, this.CompanyName, grossSalaryForYear));

    }
}
