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

    private CompanyAverageGrossSalary(Guid id, Company company, decimal grossSalary, Currency currency, int year) : base(id)
    {
        if (company is null)
        {
            throw new ArgumentNullException(nameof(company));
        }
        GrossSalary = grossSalary;
        Company = company;
        Currency = currency;
        Year = year;
        if (Year > DateTime.Now.Year)
        {
            throw new InvalidYearForAverageSalaryException("Can't enter historical data for future!");
        }
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

    /// <summary>
    /// Creates new instance of <see cref="CompanyAverageGrossSalary"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static CompanyAverageGrossSalary Create(Company company, Guid id, decimal grossSalary, int year, Currency currency)
    {
        //TODO: check if already exists for year  
        var grossSalaryForYear = new CompanyAverageGrossSalary(id: id,
                                                               company: company,
                                                               grossSalary: grossSalary,
                                                               currency: currency,
                                                               year: year);
        return grossSalaryForYear;
    }

}
