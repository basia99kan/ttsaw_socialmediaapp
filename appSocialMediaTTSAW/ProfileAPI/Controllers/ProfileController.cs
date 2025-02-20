using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileAPI.Entities;
using ProfileAPI.Dto;
using System.Security.Claims;
using System.Threading.Tasks;
using ProfileAPI.Repositories.Contracts;

namespace ProfileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public ProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet("get-profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
                return Unauthorized();

            var profile = await _userProfileRepository.GetUserProfileAsync(userId); 
            if (profile == null)
            {
                return NotFound("Profil użytkownika nie istnieje."); 
            }

            var profileDto = new UserProfileDto
            {
                Description = profile.Description,
                Hobby = profile.Hobby,
                Location = profile.Location,
                ProfilePictureUrl = profile.ProfilePictureUrl,
                Website = profile.Website
            };

            return Ok(profileDto); 
        }

        [HttpPost("add-profile")]
        public async Task<IActionResult> CreateProfile([FromBody] UserProfileDto model)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
                return Unauthorized();

            var profile = new UserProfile
            {
                UserId = userId,
                Description = model.Description,
                Hobby = model.Hobby,
                Location = model.Location,
                ProfilePictureUrl = model.ProfilePictureUrl,
                Website = model.Website
            };

            try
            {
                var createdProfile = await _userProfileRepository.CreateUserProfileAsync(profile); // Tworzenie profilu
                var profileDto = new UserProfileDto
                {
                    Description = createdProfile.Description,
                    Hobby = createdProfile.Hobby,
                    Location = createdProfile.Location,
                    ProfilePictureUrl = createdProfile.ProfilePictureUrl,
                    Website = createdProfile.Website
                };

                return Ok(profileDto); 
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileDto model)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
                return Unauthorized();

            var profile = new UserProfile
            {
                UserId = userId,
                Description = model.Description,
                Hobby = model.Hobby,
                Location = model.Location,
                ProfilePictureUrl = model.ProfilePictureUrl,
                Website = model.Website
            };

            try
            {
                var updatedProfile = await _userProfileRepository.UpdateUserProfileAsync(profile); // Aktualizowanie profilu
                var profileDto = new UserProfileDto
                {
                    Description = updatedProfile.Description,
                    Hobby = updatedProfile.Hobby,
                    Location = updatedProfile.Location,
                    ProfilePictureUrl = updatedProfile.ProfilePictureUrl,
                    Website = updatedProfile.Website
                };

                return Ok(profileDto); 
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}