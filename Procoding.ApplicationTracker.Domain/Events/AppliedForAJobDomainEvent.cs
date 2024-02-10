using Procoding.ApplicationTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Events
{
    /// <summary>
    /// Represents event which happens when candidate is applied for a job.
    /// </summary>
    public class AppliedForAJobDomainEvent : IDomainEvent
    {
        public AppliedForAJobDomainEvent(JobApplication jobApplication)
        {
            JobApplication = jobApplication;
        }

        public JobApplication JobApplication { get; }
    }
}
