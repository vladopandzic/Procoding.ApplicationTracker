namespace Procoding.ApplicationTracker.DTOs.Model;

public class CandidateDTO
{
    public CandidateDTO(string name, string surname, string email)
    {
        Name = name;
        Surname = surname;
        Email = email;
    }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }
}
