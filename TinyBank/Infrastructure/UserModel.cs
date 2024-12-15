using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure
{
    public class UserModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int UserType { get; set; }

        public string Document { get; set; }

        public string Country { get; set; }

        public AccountModel Account { get; set; }

        public bool Active { get; set; }
    }
}
