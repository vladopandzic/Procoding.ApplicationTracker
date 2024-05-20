using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.Validators;

namespace Procoding.ApplicationTracker.Web.ViewModels.Companies;

public class CompanyDetailsViewModel : ViewModelBase
{
    private readonly ICompanyService _companyService;

    public CompanyDTO? Company { get; set; }

    public CompanyValidator Validator { get; }

    public CompanyDetailsViewModel(ICompanyService companyService,
                                   CompanyValidator validator)
    {
        _companyService = companyService;
        Validator = validator;
    }

    public async Task InitializeViewModel(Guid? id, CancellationToken cancellationToken = default)
    {
        if (id is null)
        {
            Company = new CompanyDTO(Guid.Empty, "", "");
            return;
        }
        IsLoading = true;
        var response = await _companyService.GetCompanyAsync(id.Value);
        IsLoading = false;

        if (response is not null)
        {
            Company = response.Company;
        }
    }

    public async Task<bool> IsValidAsync()
    {
        return (await Validator.ValidateAsync(Company!)).IsValid;
    }

    public async Task SaveAsync()
    {
        if (Company.Id == Guid.Empty)
        {
            await _companyService.InsertCompanyAsync(
           new CompanyInsertRequestDTO(Company!.Name, Company.OfficialWebSiteLink));
        }
        else
        {
            await _companyService.UpdateCompanyAsync(
               new CompanyUpdateRequestDTO(Company!.Id, Company!.Name, Company.OfficialWebSiteLink));
        }

    }
}