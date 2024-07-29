namespace ProductManagement.Domain.Entities
{
    public abstract class EntityBase<TEntityId>
    {
        public TEntityId Id { get; set; }
    }
}
