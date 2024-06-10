namespace Procoding.ApplicationTracker.DTOs.Model;

public class EmployeeEditDTO
{
    public EmployeeEditDTO(Guid id, string name, string surname, string email, string password, bool updatePassword)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        UpdatePassword = updatePassword;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public bool UpdatePassword { get; set; }
}
