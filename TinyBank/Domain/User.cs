using Domain.Exceptions;

namespace Domain
{
    public class User
    {
        public Guid Id { get; }

        public string Name { get; }

        public UserType UserType { get; }

        public string Document { get; }

        public string Country { get; }

        public Account Account { get; }

        public bool Active { get; private set; }

        public User(Guid id, string name, UserType userType, string document, string country, bool active)
        {
            if (!IsValid(userType, document, country))
            {
                throw new DomainException(DomainErrorCode.InvalidUserDocument);
            }

            Id = id;
            Name = name;
            UserType = userType;
            Document = document;
            Active = active;
            Country = country;
        }

        public User(Guid id, string name, UserType userType, string document, string country, bool active, Account account)
            : this(id, name, userType, document, country, active)
        {
            if (account == null)
            {
                throw new DomainException(DomainErrorCode.AccountIsRequired);
            }

            Account = account;
        }

        private bool IsValid(UserType userType, string document, string country)
        {
            return true; // TODO: validate according to userType. Should it be regex ? Should it be by country ?
        }

        public void Deactivate()
        {
            this.Active = false;
        }
    }

    public enum UserType
    {
        Personal = 1,
        Business = 2
    }
}
