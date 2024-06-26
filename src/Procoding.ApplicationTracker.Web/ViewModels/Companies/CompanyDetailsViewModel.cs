﻿using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.Validators;
using Procoding.ApplicationTracker.Web.ViewModels.Abstractions;

namespace Procoding.ApplicationTracker.Web.ViewModels.Companies;

public class CompanyDetailsViewModel : EditViewModelBase
{
    private readonly ICompanyService _companyService;
    private readonly INotificationService _notificationService;

    public CompanyDTO? Company { get; set; }

    public CompanyValidator Validator { get; }

    public string? PageTitle { get; set; }


    public CompanyDetailsViewModel(ICompanyService companyService, INotificationService notificationService, CompanyValidator validator)
    {
        _companyService = companyService;
        _notificationService = notificationService;
        Validator = validator;
    }


    public async Task InitializeViewModel(Guid? id, CancellationToken cancellationToken = default)
    {
        if (id is null)
        {
            Company = new CompanyDTO(Guid.Empty, "", "");
            SetPageTitle();
            return;
        }
        IsLoading = true;
        var response = await _companyService.GetCompanyAsync(id.Value);
        IsLoading = false;

        if (response is not null)
        {
            Company = response.Value.Company;
        }
        SetPageTitle();
    }


    public async Task<bool> IsValidAsync()
    {
        return (await Validator.ValidateAsync(Company!)).IsValid;
    }

    public async Task SaveAsync()
    {
        if (!(await IsValidAsync()))
        {
            return;
        }

        IsSaving = true;

        if (Company!.Id == Guid.Empty)
        {
            var result = await _companyService.InsertCompanyAsync(new CompanyInsertRequestDTO(Company!.CompanyName, Company.OfficialWebSiteLink));

            if (result.IsSuccess)
            {
                Company.Id = result.Value.Company.Id;
            }
            _notificationService.ShowMessageFromResult(result);
        }
        else
        {
            var result = await _companyService.UpdateCompanyAsync(new CompanyUpdateRequestDTO(Company!.Id, Company!.CompanyName, Company.OfficialWebSiteLink));

            _notificationService.ShowMessageFromResult(result);
        }

        IsSaving = false;
    }

    private void SetPageTitle()
    {
        PageTitle = Company?.Id == Guid.Empty ? "New company" : $"Edit company: {Company!.CompanyName}";
    }
}