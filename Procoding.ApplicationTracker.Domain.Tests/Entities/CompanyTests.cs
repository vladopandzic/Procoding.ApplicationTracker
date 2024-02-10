using NUnit.Framework;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Events;
using Procoding.ApplicationTracker.Domain.Exceptions;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Tests.Entities;

[TestFixture]
public class CompanyTests
{
    [Test]
    public void Create_ValidArguments_ReturnsCompany()
    {
        // Arrange
        CompanyName companyName = new CompanyName("Test Company");
        Link officialWebSiteLink = new Link("https://example.com");

        // Act
        var company = Company.Create(companyName, officialWebSiteLink);

        // Assert
        Assert.That(company, Is.Not.Null);
        Assert.That(company.CompanyName, Is.EqualTo(companyName));
        Assert.That(company.OfficialWebSiteLink, Is.EqualTo(officialWebSiteLink));
    }

    [Test]
    public void Create_NullCompanyName_ThrowsArgumentException()
    {
        // Arrange
        CompanyName? companyName = null;
        Link officialWebSiteLink = new Link("https://example.com");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => Company.Create(companyName!, officialWebSiteLink));
    }

    [Test]
    public void Create_NullOfficialWebSiteLink_ThrowsArgumentException()
    {
        // Arrange
        CompanyName companyName = new CompanyName("Test Company");
        Link? officialWebSiteLink = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => Company.Create(companyName, officialWebSiteLink!));
    }

    [Test]
    public void AddGrossSalaryForYear_FutureYear_ThrowsInvalidYearForAverageSalaryException()
    {
        // Arrange
        CompanyName companyName = new CompanyName("Test Company");
        Link officialWebSiteLink = new Link("https://example.com");
        var company = Company.Create(companyName, officialWebSiteLink);

        decimal grossSalary = 50000;
        int futureYear = DateTime.Now.Year + 1; // Year in future
        Currency currency = Currency.USD;

        // Act & Assert
        Assert.Throws<InvalidYearForAverageSalaryException>(() => company.AddGrossSalaryForYear(grossSalary, futureYear, currency));
    }

    [Test]
    public void AddGrossSalaryForYear_NullCompany_ThrowsArgumentNullException()
    {
        // Arrange
        CompanyName companyName = new CompanyName("Test Company");
        Link officialWebSiteLink = new Link("https://example.com");
        var company = Company.Create(companyName, officialWebSiteLink);

        decimal grossSalary = 50000;
        int year = 2023;
        Currency currency = Currency.USD;
        Company? nullCompany = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => CompanyAverageGrossSalary.Create(nullCompany!, Guid.NewGuid(), grossSalary, year, currency));
    }

    [Test]
    public void Update_ValidArguments_UpdatesCompanyInfo()
    {
        // Arrange
        CompanyName originalCompanyName = new CompanyName("Original Company");
        Link originalWebSiteLink = new Link("https://original.example.com");
        var company = Company.Create(originalCompanyName, originalWebSiteLink);

        CompanyName updatedCompanyName = new CompanyName("Updated Company");
        Link updatedWebSiteLink = new Link("https://updated.example.com");

        // Act
        company.Update(updatedCompanyName, updatedWebSiteLink);

        // Assert
        Assert.That(company.CompanyName, Is.EqualTo(updatedCompanyName));
        Assert.That(company.OfficialWebSiteLink, Is.EqualTo(updatedWebSiteLink));
    }

    [Test]
    public void AddGrossSalaryForYear_ValidArguments_AddsSalaryForYear()
    {
        // Arrange
        CompanyName companyName = new CompanyName("Test Company");
        Link officialWebSiteLink = new Link("https://example.com");
        var company = Company.Create(companyName, officialWebSiteLink);

        decimal grossSalary = 50000;
        int year = 2023;
        Currency currency = Currency.USD;

        // Act
        company.AddGrossSalaryForYear(grossSalary, year, currency);

        // Assert
        Assert.That(company.CompanyAverageGrossSalaries, Has.Count.EqualTo(1));
        var addedSalary = company.CompanyAverageGrossSalaries[0];
        Assert.That(addedSalary.GrossSalary, Is.EqualTo(grossSalary));
        Assert.That(addedSalary.Year, Is.EqualTo(year));
        Assert.That(addedSalary.Currency, Is.EqualTo(currency));
    }

    [Test]
    public void AddGrossSalaryForYear_ValidArguments_AddsSalaryForYearAndAddsDomainEvent()
    {
        // Arrange
        CompanyName companyName = new CompanyName("Test Company");
        Link officialWebSiteLink = new Link("https://example.com");
        var company = Company.Create(companyName, officialWebSiteLink);

        decimal grossSalary = 50000;
        int year = 2023;
        Currency currency = Currency.USD;

        // Act
        company.AddGrossSalaryForYear(grossSalary, year, currency);

        // Assert
        Assert.That(company.CompanyAverageGrossSalaries, Has.Count.EqualTo(1));
        var addedSalary = company.CompanyAverageGrossSalaries[0];
        Assert.That(addedSalary.GrossSalary, Is.EqualTo(grossSalary));
        Assert.That(addedSalary.Year, Is.EqualTo(year));
        Assert.That(addedSalary.Currency, Is.EqualTo(currency));

        // Check if domain event is added
        Assert.That(company.DomainEvents, Has.Count.EqualTo(2));
        Assert.That(company.DomainEvents.Skip(1).Take(1).FirstOrDefault(), Is.InstanceOf<GrossSalaryForCompanyAddedDomainEvent>());
    }
}

