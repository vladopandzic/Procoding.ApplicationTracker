using Procoding.ApplicationTracker.DTOs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.DTOs.Response.Candidates;

public class CandidateUpdatedResponseDTO
{
    public CandidateUpdatedResponseDTO(CandidateDTO candidate)
    {
        Candidate = candidate;
    }

    public CandidateUpdatedResponseDTO()
    {
    }

    public CandidateDTO Candidate { get; set; }
}
