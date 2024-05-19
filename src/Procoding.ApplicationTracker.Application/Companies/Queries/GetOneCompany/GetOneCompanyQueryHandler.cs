using MapsterMapper;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Application.Companies.Queries.GetOneCompany;

internal class GetOneCompanyQueryHandler : IQueryHandler<GetOneCompanyQuery, CompanyResponseDTO>
{
    private readonly ICompanyRepository _companyRepository;

    public GetOneCompanyQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<CompanyResponseDTO> Handle(GetOneCompanyQuery request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetCompanyAsync(request.Id, cancellationToken);

        if (company is null)
            throw new Domain.Exceptions.CompanyDoesNotExistException();

        var companyDTO = new CompanyDTO(company.CompanyName.Value, company.OfficialWebSiteLink.Value);

        return new CompanyResponseDTO(companyDTO);
    }
}
