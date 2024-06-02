using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Request.JobApplicationSources;
using Procoding.ApplicationTracker.Web.Services;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.Validators;
using Procoding.ApplicationTracker.Web.ViewModels.Abstractions;

namespace Procoding.ApplicationTracker.Web.ViewModels.Employees;

public class EmployeeDetailsViewModel : EditViewModelBase
{
    private readonly IEmployeeService _employeeService;
    private readonly INotificationService _notificationService;

    public EmployeeDTO? Employee { get; set; }

    public EmployeeValidator Validator { get; }

    public EmployeeDetailsViewModel(IEmployeeService employeeService, EmployeeValidator validator, INotificationService notificationService)
    {
        _employeeService = employeeService;
        Validator = validator;
        _notificationService = notificationService;
    }

    public async Task InitializeViewModel(Guid? id, CancellationToken cancellationToken = default)
    {
        if (id is null)
        {
            Employee = new EmployeeDTO(Guid.Empty, "", "", "", "");
            return;
        }
        IsLoading = true;
        var response = await _employeeService.GetEmployeeAsync(id.Value);
        IsLoading = false;

        if (response.IsSuccess)
        {
            Employee = response.Value.Employee;
        }
    }

    public async Task<bool> IsValidAsync()
    {
        return (await Validator.ValidateAsync(Employee!)).IsValid;
    }

    public async Task SaveAsync()
    {
        if (!(await IsValidAsync()))
        {
            return;
        }

        IsSaving = true;

        if (Employee!.Id == Guid.Empty)
        {
            var result = await _employeeService.InsertEmployeeAsync(new EmployeeInsertRequestDTO(Employee!.Name,
                                                                                                 Employee.Surname,
                                                                                                 Employee.Email,
                                                                                                 Employee.Password));

            if (result.IsSuccess)
            {
                Employee.Id = result.Value.Employee.Id;
            }

            _notificationService.ShowMessageFromResult(result);
        }
        else
        {
            var result = await _employeeService.UpdateEmployeeAsync(new EmployeeUpdateRequestDTO(Employee!.Id,
                                                                                                 Employee!.Name,
                                                                                                 Employee.Surname,
                                                                                                 Employee.Email,
                                                                                                 Employee.Password));

            _notificationService.ShowMessageFromResult(result);
        }

        IsSaving = false;
    }
}