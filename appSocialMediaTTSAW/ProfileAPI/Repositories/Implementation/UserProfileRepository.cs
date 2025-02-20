using Microsoft.EntityFrameworkCore;
using ProfileAPI.Data;
using ProfileAPI.Entities;
using ProfileAPI.Repositories.Contracts;

namespace ProfileAPI.Repositories.Implementation
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ProfileDbContext _context;

        public UserProfileRepository(ProfileDbContext context)
        {
            _context = context;
        }

    
        public async Task<UserProfile> GetUserProfileAsync(int userId)
        {
            return await _context.UserProfiles
                .FirstOrDefaultAsync(up => up.UserId == userId);
        }

        public async Task<UserProfile> CreateUserProfileAsync(UserProfile profile)
        {
            var existingProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(up => up.UserId == profile.UserId);

            if (existingProfile != null)
            {
                throw new System.Exception("Profil użytkownika już istnieje.");
            }

            _context.UserProfiles.Add(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<UserProfile> UpdateUserProfileAsync(UserProfile profile)
        {
            var existingProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(up => up.UserId == profile.UserId);

            if (existingProfile == null)
            {
                throw new System.Exception("Profil użytkownika nie istnieje.");
            }

            existingProfile.Description = profile.Description;
            existingProfile.Location = profile.Location;
            existingProfile.ProfilePictureUrl = profile.ProfilePictureUrl;
            existingProfile.Website = profile.Website;
            existingProfile.Hobby = profile.Hobby;

            await _context.SaveChangesAsync();
            return existingProfile;
        }
    }
}