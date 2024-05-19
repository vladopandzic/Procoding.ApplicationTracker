using Procoding.ApplicationTracker.DTOs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.DTOs.Response.Candidates;

public class CandidateInsertedResponseDTO
{
    public CandidateInsertedResponseDTO(CandidateDTO candidate)
    {
        Candidate = candidate;
    }

    public CandidateInsertedResponseDTO()
    {

    }
    public CandidateDTO Candidate { get; set; }
}
