using Ardalis.Specification;
using Procoding.ApplicationTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Repositories;

public interface IEmployeeRepository
{
    Task<int> CountAsync(ISpecification<Employee> spec, CancellationToken cancellationToken);

    /// <summary>
    /// If already exist with same name.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(string email, Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Inserts the specified employee to the database.
    /// </summary>
    /// <param name="Employee">The employee to be inserted to the database.</param>
    Task InsertAsync(Employee employee, CancellationToken cancellationToken);

    /// <summary>
    /// Gets list of employees.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Employee>> GetEmployeesAsync(ISpecification<Employee> spec, CancellationToken cancellationToken);

    /// <summary>
    /// Get one company.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Employee?> GetEmployeeAsync(Guid id, CancellationToken cancellationToken);
}
