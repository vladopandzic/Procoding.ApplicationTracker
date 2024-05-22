using MudBlazor;
using Procoding.ApplicationTracker.DTOs.Request.Base;

namespace Procoding.ApplicationTracker.Web;

public class GridStateConverter
{
    public static TResponse ConvertToRequest<TResponse, TRequest>(GridState<TRequest> state) where TResponse : BaseListingRequestDTO, new()
    {
        var baseRequest = new BaseListingRequestDTO
        {
            PageSize = state.PageSize,
            PageNumber = state.Page + 1,
            Sort = state.SortDefinitions.Select(x => new SortModelDTO
            {
                SortBy = x.SortBy,
                Descending = x.Descending
            }).ToList(),
            Filters = state.FilterDefinitions.Select(x => new FilterModelDto
            {
                Key = x.Title,
                Value = x.Value as string,
                Operator = x.Operator
            }).ToList()
        };

        var derivedRequest = new TResponse();

        // Copy properties from the base request to the derived request
        CopyProperties(baseRequest, derivedRequest);

        return derivedRequest;
    }

    private static void CopyProperties<T>(BaseListingRequestDTO source, T destination) where T : BaseListingRequestDTO
    {
        destination.PageSize = source.PageSize;
        destination.PageNumber = source.PageNumber;
        destination.Sort = source.Sort;
        destination.Filters = source.Filters;

        // Add additional properties to copy if needed
    }
}
