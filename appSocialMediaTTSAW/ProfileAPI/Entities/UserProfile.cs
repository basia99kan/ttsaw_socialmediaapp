//using IdentityAPI.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileAPI.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
// public  ApplicationUser? User { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; } 
        public string? ProfilePictureUrl { get; set; } 
        public string? Website { get; set; }

        public string? Hobby { get; set; }
    }
}
