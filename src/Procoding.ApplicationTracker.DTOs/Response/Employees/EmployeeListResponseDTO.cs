using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response.Employees;

public class EmployeeListResponseDTO
{
    public EmployeeListResponseDTO()
    {

    }
    public EmployeeListResponseDTO(IReadOnlyList<EmployeeDTO> employees, int totalCount)
    {
        Employees = employees;
        TotalCount = totalCount;
    }

    public IReadOnlyList<EmployeeDTO> Employees { get; set; }

    public int TotalCount { get; set; }
}
