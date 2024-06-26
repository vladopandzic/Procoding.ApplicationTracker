﻿using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Application.Companies.Commands.InsertCompany;

internal sealed class InsertCompanyCommandHandler : ICommandHandler<InsertCompanyCommand, CompanyInsertedResponseDTO>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InsertCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CompanyInsertedResponseDTO>> Handle(InsertCompanyCommand request, CancellationToken cancellationToken)
    {
        var companyName = new CompanyName(request.Name);
        var officialWebSiteLink = new Link(request.OfficialWebSiteLink);
        var company = Company.Create(companyName, officialWebSiteLink);

        await _companyRepository.InsertAsync(company, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //TODO: in case of failure
        var companyDTO = new CompanyDTO(company.Id, company.CompanyName.Value, company.OfficialWebSiteLink.Value);

        return new CompanyInsertedResponseDTO(companyDTO);
    }
}
