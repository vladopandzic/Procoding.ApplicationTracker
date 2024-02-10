using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Infrastructure.Configurations;

/// <summary>
/// This class is used to configure the <see cref="Company"/> entity. It is used to map the <see cref="Company"/> entity
/// to a database table.
/// </summary>
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.CompanyAverageGrossSalaries)
               .WithOne(x => x.Company)
               .OnDelete(DeleteBehavior.Cascade);

        builder.ComplexProperty(x => x.OfficialWebSiteLink)
               .IsRequired()
               .Property(x => x.Value)
               .IsRequired()
               .HasMaxLength(Link.MaxLengthForValue);

        builder.ComplexProperty(x => x.CompanyName)
               .IsRequired()
               .Property(x => x.Value)
               .IsRequired()
               .HasMaxLength(CompanyName.MaxLengthForName);
    }
}

