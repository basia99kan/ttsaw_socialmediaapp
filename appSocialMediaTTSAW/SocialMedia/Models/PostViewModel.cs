namespace SocialMedia.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Fullname { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
