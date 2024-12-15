namespace Application.UserFeatures
{
    public class CreateUserDto
    {
        public string Name { get; set; }

        public string Document { get; set; }

        public string Country { get; set; }

        public UserTypeDto UserType { get; set; }
    }

    public enum UserTypeDto
    {
        Personal = 1,
        Business = 2
    }
}
