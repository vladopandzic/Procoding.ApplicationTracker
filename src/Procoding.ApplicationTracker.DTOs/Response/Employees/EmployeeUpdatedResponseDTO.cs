using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response.Employees;

public class EmployeeUpdatedResponseDTO
{
    public EmployeeUpdatedResponseDTO(EmployeeDTO employee)
    {
        Employee = employee;
    }

    public EmployeeUpdatedResponseDTO()
    {
    }

    public EmployeeDTO Employee { get; set; }
}
