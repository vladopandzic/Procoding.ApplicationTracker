using Procoding.ApplicationTracker.DTOs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.DTOs.Response.Companies;

public class CompanyInsertedResponseDTO
{
    public CompanyInsertedResponseDTO(CompanyDTO company)
    {
        Company = company;
    }

    public CompanyInsertedResponseDTO()
    {
            
    }
    public CompanyDTO Company { get; set; }
}
