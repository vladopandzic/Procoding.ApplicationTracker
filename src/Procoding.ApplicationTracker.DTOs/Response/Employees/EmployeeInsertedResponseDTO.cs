using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response.Employees;

public class EmployeeInsertedResponseDTO
{
    public EmployeeInsertedResponseDTO(EmployeeDTO employee)
    {
        Employee = employee;
    }

    public EmployeeInsertedResponseDTO()
    {

    }
    public EmployeeDTO Employee { get; set; }
}
