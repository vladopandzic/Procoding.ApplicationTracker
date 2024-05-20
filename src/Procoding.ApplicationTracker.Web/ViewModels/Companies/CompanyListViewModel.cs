using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.ViewModels.Companies;

public class CompanyListViewModel : ViewModelBase
{
    private readonly ICompanyService _companyService;

    public IReadOnlyCollection<CompanyDTO> Companies { get; set; } = new List<CompanyDTO>();

    public CompanyListViewModel(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    public async Task InitializeViewModel(CancellationToken cancellationToken = default)
    {
        IsLoading = true;
        var response = await _companyService.GetCompaniesAsync(cancellationToken);
        IsLoading = false;

        if (response is not null)
        {
            Companies = response.Companies;
        }
    }
}