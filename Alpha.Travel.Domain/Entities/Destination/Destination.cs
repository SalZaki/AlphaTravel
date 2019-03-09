namespace Alpha.Travel.Domain.Entities.Destination
{
    public sealed class Destination : BaseEntity
    {
        public Destination()
        {
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}