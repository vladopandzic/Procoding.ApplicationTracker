using Procoding.ApplicationTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Events
{
    /// <summary>
    /// Represents event which happens when new candidate is created.
    /// </summary>
    public class CandidateCreatedDomainEvent : IDomainEvent
    {
        public CandidateCreatedDomainEvent(Candidate candidate)
        {
            Candidate = candidate;
        }

        public Candidate Candidate { get; }
    }
}
