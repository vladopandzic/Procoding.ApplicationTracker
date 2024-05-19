using Procoding.ApplicationTracker.DTOs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.DTOs.Response.Candidates;

public class CandidateListResponseDTO
{
    public CandidateListResponseDTO(IReadOnlyList<CandidateDTO> candidates)
    {
        Candidates = candidates;
    }

    public CandidateListResponseDTO()
    {

    }
    public IReadOnlyList<CandidateDTO> Candidates { get; set; }
}
