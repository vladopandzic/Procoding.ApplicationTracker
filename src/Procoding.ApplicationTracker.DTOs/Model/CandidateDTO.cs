namespace Procoding.ApplicationTracker.DTOs.Model;

public class CandidateDTO
{
    public CandidateDTO(Guid id, string name, string surname, string email, string password)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
