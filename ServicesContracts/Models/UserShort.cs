using System.Text.Json.Serialization;

namespace ServicesContracts.Models
{
    public class UserShort
    {
        public int Id { get; set; }

        public string Login { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
