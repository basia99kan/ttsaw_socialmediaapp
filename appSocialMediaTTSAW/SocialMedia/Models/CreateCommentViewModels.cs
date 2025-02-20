using System.ComponentModel.DataAnnotations;


namespace SocialMedia.Models
{
    public class CreateCommentViewModels
    {
       
        public int PostId { get; set; }

        [Required(ErrorMessage = "Komentarz nie może być pusty.")]
        public string Content { get; set; }
    }
}
