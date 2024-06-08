using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Procoding.ApplicationTracker.Web.ViewModels.Employees;

namespace Procoding.ApplicationTracker.Web.Pages.Employee;

[Authorize(Roles = "Employee")]
public partial class EmployeeDetailsPage
{
    [Inject]
    public EmployeeDetailsViewModel ViewModel { get; set; } = default!;

    [Parameter]
    public string Id { get; set; }

    protected MudForm? mudForm;

    protected override async Task OnInitializedAsync()
    {
        if (Id == "new")
        {
            await ViewModel.InitializeViewModel(null);
        }
        else
        {
            await ViewModel.InitializeViewModel(Guid.Parse(Id));
        }
        await base.OnInitializedAsync();
    }

    protected async Task OnSubmit()
    {
        await mudForm!.Validate();

        await ViewModel.SaveAsync();

    }
}
