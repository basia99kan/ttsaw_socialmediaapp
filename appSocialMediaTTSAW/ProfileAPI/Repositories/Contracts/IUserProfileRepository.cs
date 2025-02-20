using ProfileAPI.Entities;

namespace ProfileAPI.Repositories.Contracts
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> GetUserProfileAsync(int userId); 
        Task<UserProfile> CreateUserProfileAsync(UserProfile profile);
        Task<UserProfile> UpdateUserProfileAsync(UserProfile profile);
    }
}

