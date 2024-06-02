using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Infrastructure.Data;

namespace Procoding.ApplicationTracker.Infrastructure.Repositories;

public class EmployeesRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbContext;
    readonly UserManager<Employee> _employeeManager;

    public EmployeesRepository(ApplicationDbContext dbContext, UserManager<Employee> employeeManager)
    {
        _employeeManager = employeeManager;
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

    public async Task<IdentityResult> InsertAsync(Employee employee, string password, CancellationToken cancellationToken)
    {
        return await _employeeManager.CreateAsync(employee, password);
    }

    public async Task<bool> ExistsAsync(string email, Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Employees.AnyAsync(x => x.Email.Value == email && x.Id != id);
    }
}
