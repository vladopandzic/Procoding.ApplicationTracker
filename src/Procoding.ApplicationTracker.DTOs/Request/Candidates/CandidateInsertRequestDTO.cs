using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.DTOs.Request.Candidates;

public record CandidateInsertRequestDTO(string Name, string Surname, string Email);