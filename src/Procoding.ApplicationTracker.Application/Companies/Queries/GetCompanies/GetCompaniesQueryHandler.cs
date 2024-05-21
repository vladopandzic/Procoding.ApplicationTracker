using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Application.Companies.Queries.GetCompanies;

internal sealed class GetCompaniesQueryHandler : IQueryHandler<GetCompaniesQuery, CompanyListResponseDTO>
{
    private readonly ICompanyRepository _companyRepository;

    public GetCompaniesQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<CompanyListResponseDTO> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.GetCompaniesAsync(cancellationToken);

        var companyDTOs = companies.Select(x => new CompanyDTO(x.Id, x.CompanyName.Value, x.OfficialWebSiteLink.Value)).ToList();

        return new CompanyListResponseDTO(companyDTOs.AsReadOnly());
    }
}
