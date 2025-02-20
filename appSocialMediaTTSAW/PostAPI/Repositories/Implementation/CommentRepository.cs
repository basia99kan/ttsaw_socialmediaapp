using PostAPI.Entities;
using PostAPI.Responses;
using Microsoft.EntityFrameworkCore;
using PostAPI.Data;
using PostAPI.Repositories.Contracts;

namespace PostAPI.Repositories.Implementation
{
    public class CommentRepository : IGenericRepositoryInterface<Comment>
    {
        private readonly PostDbContext _postDbContext;

        public CommentRepository(PostDbContext postDbContext)
        {
            _postDbContext = postDbContext;
        }
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var comment = await _postDbContext.Comments.FindAsync(id);
            if (comment is null) return NotFound();

            _postDbContext.Comments.Remove(comment);
            await Commit();
            return Success();
        }

        //Komentarze dla danego posta 
        public async Task<List<Comment>> GetAll()
        {
            return await _postDbContext.Comments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Comment> GetById(int id)
        {
            return await _postDbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<GeneralResponse> Insert(Comment comment)
        {
            _postDbContext.Comments.Add(comment);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Comment comment)
        {
            var existingComment = await _postDbContext.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);
            if (existingComment is null) return new GeneralResponse(false, "Comment does not exist");

            existingComment.Content = comment.Content;

            await _postDbContext.SaveChangesAsync();
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Comment not found");
        private static GeneralResponse Success() => new(true, "Process completed");

        private async Task Commit() => await _postDbContext.SaveChangesAsync();
    }
}