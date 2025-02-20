using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class CreatePostViewModel
    {
        [Required(ErrorMessage = "Post nie może być pusty.")]
        public string Content { get; set; }
    }
}
