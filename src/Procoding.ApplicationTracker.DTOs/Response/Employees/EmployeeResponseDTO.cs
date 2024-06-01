using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response.Employees;

public class EmployeeResponseDTO
{
    public EmployeeResponseDTO(EmployeeDTO employee)
    {
        Employee = employee;
    }
    public EmployeeResponseDTO()
    {

    }

    public EmployeeDTO Employee { get; set; }
}
