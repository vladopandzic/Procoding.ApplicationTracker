using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Common;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Domain.Entities;

/// <summary>
/// Represents company the candidate applies for.
/// </summary>
public sealed class Company : EntityBase, ISoftDeletableEntity, IAuditableEntity
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
    public Company(Guid id, CompanyName companyName, Link officialWebSiteLink) : base(id)
    {
        CompanyName = companyName;
        OfficialWebSiteLink = officialWebSiteLink;
    }

    /// <summary>
    /// Company name. Must be at most <see cref="CompanyName.MaxLengthForName"/> long.
    /// </summary>
    public CompanyName CompanyName { get; }

    /// <summary>
    /// Official website. Must be valid link. Must be at most <see cref="Link.MaxLengthForValue"/> long."/>
    /// </summary>
    public Link OfficialWebSiteLink { get; }

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
}
