﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.Web.ViewModels.JobApplications;

namespace Procoding.ApplicationTracker.Web.Pages.JobApplication;

[Authorize]
public partial class MyJobApplicationPage
{
    [Inject]
    public MyJobApplicationViewModel ViewModel { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    [Inject]
    public IDialogService DialogService { get; set; } = default!;

    [Parameter]
    public string Id { get; set; }

    protected MudForm? mudForm;

    private MudDialog dialogRef = default!;

    private MudForm? newNewCompanyForm = default!;

    private MudAutocomplete<CompanyDTO> mudCompanyAutocomplete = default!;

    protected override async Task OnParametersSetAsync()
    {
        if (ViewModel.JobApplication?.Id != null && Id == ViewModel.JobApplication?.Id.ToString())
        {
            return;
        }
        if (Id == "new")
        {
            await ViewModel.InitializeViewModel(null);
        }
        else
        {
            await ViewModel.InitializeViewModel(Guid.Parse(Id));
        }
        await base.OnParametersSetAsync();
    }

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

    private async Task SaveNewCompany()
    {
        await newNewCompanyForm!.Validate();
        await ViewModel.SaveNewCompany();

        if (ViewModel.JobApplication?.Company?.Id != Guid.Empty)
        {
            await mudCompanyAutocomplete.SelectOption(ViewModel.JobApplication!.Company);
        }
    }

    protected async Task OnSubmit()
    {
        await mudForm!.Validate();
        await ViewModel.SaveAsync();
    }

    private string CompaniesToStringFunc(CompanyDTO company)
    {
        return company?.CompanyName ?? string.Empty;
    }

    private string CandidatesToStringFunc(CandidateDTO candidate)
    {
        return candidate != null ? candidate.Name + " " + candidate.Surname : "";
    }

    private string JobApplicationSourcesToStringFunc(JobApplicationSourceDTO jobApplicationSource)
    {
        return jobApplicationSource != null ? jobApplicationSource.Name : "";
    }

    private string JobTypeToStringFunc(JobTypeDTO jobType)
    {
        return jobType != null ? jobType.Value : "";
    }
    private string WorkLocationToStringFunc(WorkLocationTypeDTO workLocation)
    {
        return workLocation != null ? workLocation.Value : "";
    }


    protected async Task<IEnumerable<CompanyDTO>> SearchCompaniesFunc(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return ViewModel.Companies;
        }
        return ViewModel.Companies.Where(x => x.CompanyName.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }

    private void NewValue(string newValue)
    {
        var a = "b";
    }

    protected async Task<IEnumerable<JobApplicationSourceDTO>> SearchJobApplicationSourceFunc(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return ViewModel.JobApplicationSources;
        }
        return ViewModel.JobApplicationSources.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }

    protected async Task<IEnumerable<JobTypeDTO>> SearchJobTypesFunc(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return ViewModel.JobTypes;
        }
        return ViewModel.JobTypes.Where(x => x.Value.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }

    protected async Task<IEnumerable<WorkLocationTypeDTO>> SearchWorkLocationTypesFunc(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return ViewModel.WorkLocationTypes;
        }
        return ViewModel.WorkLocationTypes.Where(x => x.Value.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }


    private void OpenCreateNewCompanyDialog()
    {
        ViewModel.CreateNewCompanyDialogVisible = true;
    }

}