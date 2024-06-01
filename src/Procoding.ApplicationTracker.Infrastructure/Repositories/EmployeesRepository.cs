using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Infrastructure.Data;

namespace Procoding.ApplicationTracker.Infrastructure.Repositories;

public class EmployeesRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Employee?> GetEmployeeAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Employee>> GetEmployeesAsync(ISpecification<Employee> spec, CancellationToken cancellationToken)
    {
        return await _dbContext.Employees.ToListAsync(spec, cancellationToken);
    }

    public async Task<int> CountAsync(ISpecification<Employee> spec, CancellationToken cancellationToken)
    {
        var query = SpecificationEvaluator.Default.GetQuery(_dbContext.Employees, spec, true);
        return await query.CountAsync();
    }

    public async Task InsertAsync(Employee Employee, CancellationToken cancellationToken)
    {
        await _dbContext.Employees.AddAsync(Employee, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string email, Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Employees.AnyAsync(x => x.Email.Value == email && x.Id != id);
    }
}
