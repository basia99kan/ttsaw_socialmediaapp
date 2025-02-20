using System.ComponentModel.DataAnnotations;


namespace IdentityAPI.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; } = string.Empty;


    }
}
