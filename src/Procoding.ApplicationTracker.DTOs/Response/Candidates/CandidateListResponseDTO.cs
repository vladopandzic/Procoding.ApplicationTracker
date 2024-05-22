using Procoding.ApplicationTracker.DTOs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.DTOs.Response.Candidates;

public class CandidateListResponseDTO
{
    public CandidateListResponseDTO()
    {

    }
    public CandidateListResponseDTO(IReadOnlyList<CandidateDTO> candidates, int totalCount)
    {
        Candidates = candidates;
        TotalCount = totalCount;
    }

    public IReadOnlyList<CandidateDTO> Candidates { get; set; }

    public int TotalCount { get; set; }
}
