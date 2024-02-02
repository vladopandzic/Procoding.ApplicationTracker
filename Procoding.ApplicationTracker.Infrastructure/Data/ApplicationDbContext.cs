using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.Entities;
using System.Reflection;

namespace Procoding.ApplicationTracker.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    //{
    //}
    public DbSet<Candidate> Candidates { get; set; }

    public DbSet<JobApplication> JobApplications { get; set; }

    public DbSet<Company> Companies { get; set; }

    public DbSet<JobApplicationSource> JobApplicationSources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=ApplicationTrackerDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}