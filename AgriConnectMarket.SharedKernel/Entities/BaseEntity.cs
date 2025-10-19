namespace AgriConnectMarket.SharedKernel.Entities
{
    public abstract class BaseEntity<TId> where TId : notnull
    {
        public TId Id { get; protected set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        protected BaseEntity() { }

        public override bool Equals(object? obj)
        {
            if (obj is not BaseEntity<TId> other) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode() => HashCode.Combine(GetType(), Id);
    }
}
