namespace Procoding.ApplicationTracker.Domain.Common;

public interface IEntityBase
{
    /// <inheritdoc/>
    bool Equals(object? obj);

    /// <inheritdoc/>
    int GetHashCode();

    /// <summary>
    /// Gets or sets the entity identifier.
    /// </summary>
    Guid Id { get; }
}
