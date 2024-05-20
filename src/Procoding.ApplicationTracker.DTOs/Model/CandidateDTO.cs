namespace Procoding.ApplicationTracker.DTOs.Model;

public class CandidateDTO
{
    public CandidateDTO(Guid id, string name, string surname, string email)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }
}
