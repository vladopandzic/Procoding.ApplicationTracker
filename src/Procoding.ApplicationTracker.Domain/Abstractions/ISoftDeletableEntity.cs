namespace Procoding.ApplicationTracker.Domain.Abstractions;

/// <summary>
/// Represents the marker interface for soft-deletable entities.
/// </summary>
public interface ISoftDeletableEntity
{
    /// <summary>
    /// Gets the date and time in UTC format the entity was deleted on.
    /// </summary>
    DateTime? DeletedOnUtc { get; }
}
