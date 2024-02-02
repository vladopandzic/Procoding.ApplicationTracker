using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Infrastructure.Configurations;

/// <summary>
/// This class is used to configure the <see cref="InterviewStep"/> entity. It is used to map the <see
/// cref="InterviewStep"/> entity. to a database table.
/// </summary>
internal sealed class InterviewStepConfiguration : IEntityTypeConfiguration<InterviewStep>
{
    void IEntityTypeConfiguration<InterviewStep>.Configure(EntityTypeBuilder<InterviewStep> builder)
    {
        builder.ToTable(nameof(JobApplication.InterviewSteps));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.InteviewStepType)
               .HasConversion<string>();

        builder.Property(x => x.Description)
               .HasMaxLength(InterviewStep.MaxLengthForDescription)
               .IsRequired();

        builder.HasOne(x => x.JobApplication)
               .WithMany(x => x.InterviewSteps)
               .IsRequired();
    }
}
