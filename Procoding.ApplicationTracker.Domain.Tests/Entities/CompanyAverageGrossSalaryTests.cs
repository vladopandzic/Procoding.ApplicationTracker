using NUnit.Framework;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Exceptions;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Tests.Entities;

[TestFixture]
public class CompanyAverageGrossSalaryTests
{
    [Test]
    public void Create_ValidArguments_ReturnsCompanyAverageGrossSalary()
    {
        // Arrange
        Company company = Company.Create(new CompanyName("Test Company"), new Link("https://example.com"));
        Guid id = Guid.NewGuid();
        decimal grossSalary = 50000;
        Currency currency = Currency.USD;
        int year = DateTime.Now.Year;

        // Act
        var averageGrossSalary = CompanyAverageGrossSalary.Create(company, id, grossSalary, year, currency);

        // Assert
        Assert.That(averageGrossSalary, Is.Not.Null);
        Assert.That(averageGrossSalary.Company, Is.EqualTo(company));
        Assert.That(averageGrossSalary.GrossSalary, Is.EqualTo(grossSalary));
        Assert.That(averageGrossSalary.Currency, Is.EqualTo(currency));
        Assert.That(averageGrossSalary.Year, Is.EqualTo(year));
    }

    [Test]
    public void Create_NullCompany_ThrowsArgumentNullException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        decimal grossSalary = 50000;
        Currency currency = Currency.USD;
        int year = DateTime.Now.Year;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => CompanyAverageGrossSalary.Create(null, id, grossSalary, year, currency));
    }

    [Test]
    public void Create_FutureYear_ThrowsInvalidYearForAverageSalaryException()
    {
        // Arrange
        Guid companyId = Guid.NewGuid();
        Company company = Company.Create(new CompanyName("Test Company"), new Link("https://example.com"));
        Guid id = Guid.NewGuid();
        decimal grossSalary = 50000;
        Currency currency = Currency.USD;
        int futureYear = DateTime.Now.Year + 1; // Year in future

        // Act & Assert
        Assert.Throws<InvalidYearForAverageSalaryException>(() => CompanyAverageGrossSalary.Create(company, id, grossSalary, futureYear, currency));
    }
}
