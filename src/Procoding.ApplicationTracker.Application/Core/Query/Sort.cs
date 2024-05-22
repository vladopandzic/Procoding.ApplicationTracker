namespace Procoding.ApplicationTracker.Application.Core.Query;

public sealed class Sort
{
    public string SortBy { get; set; } = default!;

    public bool Descending { get; set; }
}
