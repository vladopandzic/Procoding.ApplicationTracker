namespace Procoding.ApplicationTracker.Domain.Common;

/// <summary>
/// Represetnts the base class that all entities derive from.
/// </summary>
public abstract class EntityBase : IEquatable<EntityBase>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected EntityBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected EntityBase(Guid id) : this()
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));

        Id = id;
    }

    /// <summary>
    /// Gets or sets the entity identifier.
    /// </summary>
    public Guid Id { get; }

    public static bool operator ==(EntityBase a, EntityBase b)
    {
        if(a is null && b is null)
        {
            return true;
        }

        if(a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(EntityBase a, EntityBase b) => !(a == b);

    /// <inheritdoc/>
    public bool Equals(EntityBase? other)
    {
        if(other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || Id == other.Id;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if(obj is null)
        {
            return false;
        }

        if(ReferenceEquals(this, obj))
        {
            return true;
        }

        if(obj.GetType() != GetType())
        {
            return false;
        }

        if(!(obj is EntityBase other))
        {
            return false;
        }

        if(Id == Guid.Empty || other.Id == Guid.Empty)
        {
            return false;
        }

        return Id == other.Id;
    }

    /// <inheritdoc/>
    public override int GetHashCode() => Id.GetHashCode() * 41;
}
