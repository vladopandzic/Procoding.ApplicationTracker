﻿using Ardalis.Specification;
using Microsoft.AspNetCore.Identity;
using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Domain.Repositories;

public interface ICandidateRepository
{
    Task<int> CountAsync(ISpecification<Candidate> spec, CancellationToken cancellationToken);

    /// <summary>
    /// If already exist with same name.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(string email, Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Inserts the specified candidate to the database.
    /// </summary>
    /// <param name="candidate">The candidate to be inserted to the database.</param>
    Task<IdentityResult> InsertAsync(Candidate candidate, string password, CancellationToken cancellationToken);

    /// <summary>
    /// Gets list of candidates.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Candidate>> GetCandidatesAsync(ISpecification<Candidate> spec, CancellationToken cancellationToken);

    /// <summary>
    /// Get one company.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Candidate?> GetCandidateAsync(Guid id, CancellationToken cancellationToken);
}
