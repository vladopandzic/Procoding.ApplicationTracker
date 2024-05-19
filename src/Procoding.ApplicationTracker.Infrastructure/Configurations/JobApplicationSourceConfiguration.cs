using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Infrastructure.Configurations;

/// <summary>
/// This class is used to configure the <see cref="JobApplicationSource"/> entity. It is used to map the <see
/// cref="JobApplicationSource"/> entity to a database table.
/// </summary>
public sealed class JobApplicationSourceConfiguration : IEntityTypeConfiguration<JobApplicationSource>
{
    public void Configure(EntityTypeBuilder<JobApplicationSource> builder)
    {
        builder.ToTable("JobApplicationSources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
               .HasMaxLength(JobApplicationSource.MaxLengthForName)
               .IsRequired();
    }
}
