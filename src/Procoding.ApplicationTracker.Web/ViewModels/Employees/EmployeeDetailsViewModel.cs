using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.Validators;
using Procoding.ApplicationTracker.Web.ViewModels.Abstractions;

namespace Procoding.ApplicationTracker.Web.ViewModels.Employees;

public class EmployeeDetailsViewModel : EditViewModelBase
{
    private readonly IEmployeeService _employeeService;
    private readonly INotificationService _notificationService;

    public EmployeeEditDTO? Employee { get; set; }

    public EmployeeValidator Validator { get; }

    public string? PageTitle { get; set; }


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
            Employee = new EmployeeEditDTO(Guid.Empty, "", "", "", "", false);
            SetPageTitle();

            return;
        }
        IsLoading = true;
        var response = await _employeeService.GetEmployeeAsync(id.Value);
        IsLoading = false;

        if (response.IsSuccess)
        {
            var employeeDto = response.Value.Employee;
            Employee = new EmployeeEditDTO(id: employeeDto.Id,
                                           name: employeeDto.Name,
                                           surname: employeeDto.Surname,
                                           email: employeeDto.Email,
                                           password: "",
                                           updatePassword: false);
        }
        SetPageTitle();
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
                Employee.Name = result.Value.Employee.Name;
                Employee.Password = "";
                Employee.UpdatePassword = false;
            }

            _notificationService.ShowMessageFromResult(result);
        }
        else
        {
            var result = await _employeeService.UpdateEmployeeAsync(new EmployeeUpdateRequestDTO(Employee!.Id,
                                                                                                 Employee!.Name,
                                                                                                 Employee.Surname,
                                                                                                 Employee.Email,
                                                                                                 Employee.Password,
                                                                                                 Employee.UpdatePassword));

            _notificationService.ShowMessageFromResult(result);
        }

        IsSaving = false;
    }

    private void SetPageTitle()
    {
        PageTitle = Employee?.Id == Guid.Empty ? "New employee" : $"Edit employee: {Employee!.Name}";
    }
}