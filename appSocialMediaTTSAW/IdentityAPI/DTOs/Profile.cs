
using System.ComponentModel.DataAnnotations;

namespace IdentityAPI.DTOs
{

    public class Profile
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        public string Image { get; set; } = "/images/profile/default.jpg";

    }
}
