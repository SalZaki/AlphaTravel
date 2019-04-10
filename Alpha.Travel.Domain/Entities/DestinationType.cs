namespace Alpha.Travel.Domain.Entities
{
    public sealed class DestinationType : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DestinationType() { }
    }
}