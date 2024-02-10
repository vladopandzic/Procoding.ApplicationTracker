using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Infrastructure.Configurations;

/// <summary>
/// This class is used to configure the <see cref="JobApplication"/> entity. It is used to map the <see
/// cref="JobApplication"/> entity to a database table.
/// </summary>
public sealed class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        builder.ToTable("JobApplications");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.ApplicationSource);
        builder.HasOne(x => x.Candidate)
               .WithMany(x => x.JobApplications)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.InterviewSteps)
               .WithOne(x => x.JobApplication)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Company);
        builder.Property(x => x.AppliedOnUTC);

        builder.Property(x => x.JobApplicationStatus)
               .HasConversion<string>()
               .HasMaxLength(100);

    }
}
