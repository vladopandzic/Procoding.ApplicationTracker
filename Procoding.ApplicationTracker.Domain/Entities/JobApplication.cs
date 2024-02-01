namespace Procoding.ApplicationTracker.Domain.Entities;

public class JobApplication
{
    public JobApplication(DateTime appliedOnUTC, JobApplicationSource applicationSource, Company company)
    {
        AppliedOnUTC = appliedOnUTC;
        ApplicationSource = applicationSource;
        Company = company;
    }

    public DateTime AppliedOnUTC { get; }

    public JobApplicationSource ApplicationSource { get; }

    public Company Company { get; set; }

    public InterviewStep NextInterviewStep { get; } = InterviewStep.None;
}
