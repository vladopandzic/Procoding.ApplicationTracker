using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Infrastructure.Configurations;

/// <summary>
/// This class is used to configure the <see cref="CompanyAverageGrossSalary"/> entity.
/// It is used to map the  <see cref="CompanyAverageGrossSalary"/> entity to a database table.
/// </summary>
public class CompanyAverageGrossSalaryConfigurator : IEntityTypeConfiguration<CompanyAverageGrossSalary>
{
    public void Configure(EntityTypeBuilder<CompanyAverageGrossSalary> builder)
    {
        builder.ToTable(nameof(Company.CompanyAverageGrossSalaries));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Year)
               .IsRequired();
        builder.Property(x => x.GrossSalary)
               .IsRequired();
        builder.Property(x => x.Currency)
               .HasConversion<string>()
               .HasMaxLength(100);

        builder.HasOne(x => x.Company);
    }
}
