using PostAPI.Entities;
using PostAPI.Responses;
using Microsoft.EntityFrameworkCore;
using PostAPI.Data;
using PostAPI.Repositories.Contracts;

namespace PostAPI.Repositories.Implementation
{
    public class PostRepository : IGenericRepositoryInterface<Post>
    {
        private readonly PostDbContext _postDbContext;

        public PostRepository(PostDbContext postDbContext)
        {
            _postDbContext = postDbContext;
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var post = await _postDbContext.Posts.FindAsync(id);
            if (post is null) return NotFound();

            _postDbContext.Posts.Remove(post);
            await Commit();
            return Success();
        }

        public async Task<List<Post>> GetAll()
        {
            var posts =  await _postDbContext.Posts
                .AsNoTracking()
                .Include(c => c.Comments)
                .ToListAsync();
            return posts;
        }

        public async Task<Post> GetById(int id)
        {
            return await _postDbContext.Posts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //public async Task<GeneralResponse> Insert(Post post)
        // {
        // if (!await CheckDuplicatePost(post.Content!)) return new GeneralResponse(false, "Post already exists!");
        // _postDbContext.Posts.Add(post);
        // await Commit();
        // return Success();
        //  }
        public async Task<GeneralResponse> Insert(Post post)
        {
            if (post == null || string.IsNullOrEmpty(post.Content))
                return new GeneralResponse(false, "Nie można dodać pustego posta.");

            try
            {
                _postDbContext.Posts.Add(post);
                await _postDbContext.SaveChangesAsync();
                return new GeneralResponse(true, "Post dodany pomyślnie.");
            }
            catch (Exception ex)
            {
                return new GeneralResponse(false, $"Błąd podczas dodawania posta: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        public async Task<GeneralResponse> Update(Post post)
        {
            var existingPost = await _postDbContext.Posts.FirstOrDefaultAsync(p => p.Id == post.Id);
            if (existingPost is null) return new GeneralResponse(false, "Post does not exist");

            existingPost.Content = post.Content;

            await _postDbContext.SaveChangesAsync();
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Post not found");
        private static GeneralResponse Success() => new(true, "Process completed");

        private async Task Commit() => await _postDbContext.SaveChangesAsync();

        //private async Task<bool> CheckDuplicatePost(string content)
     //   {
      
        //if (string.IsNullOrEmpty(content)) return true;
        //var item = await _postDbContext.Posts.FirstOrDefaultAsync(x => x.Content != null && x.Content.ToLower() == content.ToLower());
       // return item is null;
      
            //var item = await _postDbContext.Posts.FirstOrDefaultAsync(x => x.Content!.ToLower().Equals(content.ToLower()));
           // return item is null;
        }
    }