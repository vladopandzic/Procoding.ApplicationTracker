using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Procoding.ApplicationTracker.Application.Behaviors;
using Procoding.ApplicationTracker.Application.Candidates.Commands.InsertCandidate;
using Procoding.ApplicationTracker.Application.Candidates.Commands.SignupCandidate;
using Procoding.ApplicationTracker.Application.Candidates.Commands.UpdateCandidate;
using Procoding.ApplicationTracker.Application.Companies.Commands.InsertCompany;
using Procoding.ApplicationTracker.Application.Companies.Commands.UpdateCompany;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.InsertJobApplicationSource;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.UpdateJobApplicationSource;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

namespace Procoding.ApplicationTracker.Application.Core.Extensions;

public static class ValidationExtensions
{
    public static MediatRServiceConfiguration AddValidation<TRequest, TResponse>(this MediatRServiceConfiguration config) where TRequest : notnull
    {
        return config.AddBehavior<IPipelineBehavior<TRequest, Result<TResponse>>, ValidationBehavior<TRequest, TResponse>>();
    }

    public static void AddHandlerValidations(this MediatRServiceConfiguration config)
    {
        config.AddValidation<AddJobApplicationSourceCommand, JobApplicationSourceInsertedResponseDTO>()
              .AddValidation<UpdateJobApplicationSourceCommand, JobApplicationSourceUpdatedResponseDTO>()
              .AddValidation<InsertCandidateCommand, CandidateInsertedResponseDTO>()
              .AddValidation<UpdateCandidateCommand, CandidateUpdatedResponseDTO>()
              .AddValidation<InsertCompanyCommand, CompanyInsertedResponseDTO>()
              .AddValidation<UpdateCompanyCommand, CompanyUpdatedResponseDTO>()
              .AddValidation<SignupCandidateCommand, CandidateSignupResponseDTO>();

    }
}
