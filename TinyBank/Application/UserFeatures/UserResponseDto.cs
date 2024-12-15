namespace Application.UserFeatures
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserType { get; set; }

        public string Document { get; set; }

        public string Country { get; set; }

        public bool Active { get; set; }

        public AccountResponseDto Account { get; set; }
    }

    public class AccountResponseDto
    {
        public Guid Id { get; set; }

        public decimal Balance { get; set; }
    }
}
