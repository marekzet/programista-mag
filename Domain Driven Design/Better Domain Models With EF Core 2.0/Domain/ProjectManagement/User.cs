using Domain.SharedKernel;

namespace Domain.ProjectManagement
{
    public class User : Entity, IAggregateRoot
    {
        private string name;
        private string email;

        public int Id { get; private set; }
        public Address Address { get; private set; }

        protected User()
        {
        }

        protected User(string name, string email)
        {
            this.name = name;
            this.email = email;
        }

        public static User New(string name, string email) =>
            new User(name, email);

        public void SetAddress(Address address)
        {
            if (address == null)
                throw new DomainException("Invalid address.");

            Address = address;
        }
    }
}