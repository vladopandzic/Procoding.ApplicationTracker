using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Infrastructure.Configurations;

/// <summary>
/// This class is used to configure the <see cref="Company"/> entity. It is used to map the <see cref="Company"/> entity
/// to a database table.
/// </summary>
internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.CompanyAverageGrossSalaries)
               .WithOne(x => x.Company)
               .OnDelete(DeleteBehavior.Cascade);

        builder.ComplexProperty(x => x.OfficialWebSiteLink)
               .Property(x => x.Value)
               .HasMaxLength(Link.MaxLengthForValue);

        builder.ComplexProperty(x => x.CompanyName)
               .Property(x => x.Value)
               .HasMaxLength(CompanyName.MaxLengthForName);
    }
}

