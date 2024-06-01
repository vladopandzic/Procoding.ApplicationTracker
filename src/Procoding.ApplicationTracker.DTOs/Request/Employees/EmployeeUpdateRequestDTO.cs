namespace Procoding.ApplicationTracker.DTOs.Request.Employees;

public record EmployeeUpdateRequestDTO(Guid Id, string Name, string Surname, string Email);