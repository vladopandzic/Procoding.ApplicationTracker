using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Application.Candidates.Commands.InsertCandidate;

public class InsertCandidateCommand : ICommand<CandidateInsertedResponseDTO>
{
    public InsertCandidateCommand(string name, string surname, string email)
    {
        Name = name;
        Surname = surname;
        Email = email;
    }

    public string Name { get; }

    public string Surname { get; }

    public string Email { get; }

}