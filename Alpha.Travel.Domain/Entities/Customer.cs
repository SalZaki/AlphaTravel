namespace Alpha.Travel.Domain.Entities
{
    public sealed class Customer : BaseEntity
    {
        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Customer() { }
    }
}
