using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Application.Core.Query;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Application.Employees.Queries.GetEmployees;

public sealed class GetEmployeesQuery : IQuery<EmployeeListResponseDTO>
{
    public GetEmployeesQuery(int? pageNumber, int? pageSize, List<Filter> filters, List<Sort> sort)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Filters = filters;
        Sort = sort;
    }

    public int? PageNumber { get; set; }

    public int? PageSize { get; set; }

    public List<Filter> Filters { get; set; }

    public List<Sort> Sort { get; set; }

}
