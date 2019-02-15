namespace Alpha.Travel.Domain.Entities
{
    public sealed class Destination : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Destination() { }
    }
}