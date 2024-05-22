namespace Procoding.ApplicationTracker.DTOs.Request.Base;

public class BaseListingRequestDTO
{
    public BaseListingRequestDTO()
    {
        Filters = new List<FilterModelDto>();
        Sort = new List<SortModelDTO>();
    }

    public int? PageNumber { get; set; }

    public int? PageSize { get; set; }

    public List<FilterModelDto> Filters { get; set; }

    public List<SortModelDTO> Sort { get; set; }
}
