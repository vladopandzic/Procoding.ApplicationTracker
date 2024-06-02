using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.ViewModels.Employees;

public class EmployeeListViewModel : ViewModelBase
{
    private readonly IEmployeeService _employeeService;

    public IReadOnlyCollection<EmployeeDTO> Employees { get; set; } = new List<EmployeeDTO>();

    public int TotalNumberOfEmployees { get; set; }
    public EmployeeGetListRequestDTO Request { get; set; } = new EmployeeGetListRequestDTO();

    public EmployeeListViewModel(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }


    public async Task GetEmployees(CancellationToken cancellationToken = default)
    {

        IsLoading = true;
        var response = await _employeeService.GetEmployeesAsync(Request, cancellationToken);
        IsLoading = false;

        if (response.IsSuccess)
        {
            Employees = response.Value.Employees;
            TotalNumberOfEmployees = response.Value.TotalCount;
        }
    }
}