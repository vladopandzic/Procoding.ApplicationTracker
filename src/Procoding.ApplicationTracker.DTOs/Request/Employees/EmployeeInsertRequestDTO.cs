namespace Procoding.ApplicationTracker.DTOs.Request.Employees;

public record EmployeeInsertRequestDTO(string Name, string Surname, string Email, string Password);