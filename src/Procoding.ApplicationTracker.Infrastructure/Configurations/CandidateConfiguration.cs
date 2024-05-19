using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Infrastructure.Configurations;

/// <summary>
/// This class is used to configure the Candidate entity in the database.  It implements the <see
/// cref="IEntityTypeConfiguration{TEntity}
/// </summary>
public sealed class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable("Candidates");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
               .HasMaxLength(Candidate.MaxLengthForName)
               .IsRequired();

        builder.Property(x => x.Surname)
               .HasMaxLength(Candidate.MaxLengthForSurname)
               .IsRequired();

        builder.ComplexProperty(x => x.Email)
               .IsRequired()
               .Property(x => x.Value)
               .HasMaxLength(Email.MaxLengthForValue)
               .IsRequired();

        builder.HasMany(x => x.JobApplications)
               .WithOne(x => x.Candidate)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
