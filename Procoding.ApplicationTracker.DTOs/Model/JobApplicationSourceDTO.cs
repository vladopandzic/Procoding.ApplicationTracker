namespace Procoding.ApplicationTracker.DTOs.Model;

public sealed class JobApplicationSourceDTO
{
    public JobApplicationSourceDTO(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
}
