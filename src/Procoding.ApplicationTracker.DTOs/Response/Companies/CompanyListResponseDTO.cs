using Procoding.ApplicationTracker.DTOs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.DTOs.Response.Companies;

public class CompanyListResponseDTO
{
    public CompanyListResponseDTO(IReadOnlyList<CompanyDTO> companies)
    {
        Companies = companies;
    }

    public CompanyListResponseDTO()
    {
            
    }
    public IReadOnlyList<CompanyDTO> Companies { get; set; }
}
