using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Common;
using Procoding.ApplicationTracker.Domain.Exceptions;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Domain.Entities;

/// <summary>
/// Represents average gross salary for some <see cref="Company"/> and <see cref="Year"/>.
/// </summary>
public sealed class CompanyAverageGrossSalary : EntityBase, ISoftDeletableEntity, IAuditableEntity
{
#pragma warning disable CS8618
    private CompanyAverageGrossSalary()
    {
    } //used by EF core
#pragma warning restore CS8618

    public CompanyAverageGrossSalary(Guid id, Company company, decimal grossSalary, Currency currency, int year) : base(id)
    {
        GrossSalary = grossSalary;
        Company = company;
        Currency = currency;
        if(Year > DateTime.Now.Year)
        {
            throw new InvalidYearForAverageSalaryException("Can't enter historical data for future!");
        }
        Year = year;
    }

    /// <summary>
    /// Gross salary.
    /// </summary>
    public decimal GrossSalary { get; }

    /// <summary>
    /// Currency.
    /// </summary>
    public Currency Currency { get; }

    /// <summary>
    /// Year for which this information is provided.
    /// </summary>
    public int Year { get; }

    /// <summary>
    /// Company for which this information is valid.
    /// </summary>
    public Company Company { get; }

    /// <inheritdoc/>
    public DateTime? DeletedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime CreatedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime ModifiedOnUtc { get; }
}
