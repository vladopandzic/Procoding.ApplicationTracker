namespace Procoding.ApplicationTracker.DTOs.Request.Base;

public class SortModelDTO
{
    public string SortBy { get; set; } = default!;

    public bool Descending { get; set; }
}
