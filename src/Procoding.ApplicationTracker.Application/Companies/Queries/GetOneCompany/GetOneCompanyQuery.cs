using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Application.Companies.Queries.GetOneCompany;

public sealed class GetOneCompanyQuery : IQuery<CompanyResponseDTO>
{
    public GetOneCompanyQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
