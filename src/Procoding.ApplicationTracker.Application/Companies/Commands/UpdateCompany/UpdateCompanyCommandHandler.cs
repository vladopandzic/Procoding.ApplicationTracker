using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Exceptions;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Application.Companies.Commands.UpdateCompany;

internal sealed class UpdateCompanyCommandHandler : ICommandHandler<UpdateCompanyCommand, CompanyUpdatedResponseDTO>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CompanyUpdatedResponseDTO>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var companyFromDb = await _companyRepository.GetCompanyAsync(request.Id, cancellationToken);

        //TODO: use result object
        if (companyFromDb is null)
        {
            throw new CompanyDoesNotExistException("Company does not exist");
        }

        var companyName = new CompanyName(request.CompanyName);
        var officialWebsiteLink = new Link(request.OfficialWebsiteLink);

        companyFromDb.Update(companyName, officialWebsiteLink);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //TODO: in case of failure
        var companyDto = new CompanyDTO(companyFromDb.Id, companyName.Value, officialWebsiteLink.Value);

        return new CompanyUpdatedResponseDTO(companyDto);
    }
}
