using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.Web.ViewModels.Employees;

namespace Procoding.ApplicationTracker.Web.Pages.Employee;

[Authorize]
public partial class EmployeeListPage
{
    [Inject]
    public EmployeeListViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private async Task<GridData<EmployeeDTO>> LoadGridData(GridState<EmployeeDTO> state)
    {

        ViewModel.Request = GridStateConverter.ConvertToRequest<EmployeeGetListRequestDTO, EmployeeDTO>(state);

        await ViewModel.GetEmployees();

        GridData<EmployeeDTO> data = new GridData<EmployeeDTO>
        {
            Items = ViewModel.Employees,
            TotalItems = ViewModel.TotalNumberOfEmployees
        };
        return data;
    }
}
