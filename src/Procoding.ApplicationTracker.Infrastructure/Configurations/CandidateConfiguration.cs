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


        builder.Property(x => x.ConcurrencyStamp).HasMaxLength(256);

        builder.Property(x => x.NormalizedEmail).HasMaxLength(256);

        builder.Property(x => x.UserName).HasMaxLength(512);

        builder.Property(x => x.NormalizedUserName).HasMaxLength(512);

        builder.Property(x => x.PasswordHash).HasMaxLength(1024);

        builder.Property(x => x.SecurityStamp).HasMaxLength(1024);

        builder.Property(x => x.PhoneNumber).HasMaxLength(256);
    }
}
