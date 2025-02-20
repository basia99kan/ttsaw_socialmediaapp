using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using System.Security.Claims;

namespace SocialMedia.Controllers
{
    public class ProfileController : Controller
    {
        private readonly HttpClient _profileApiClient;
        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _profileApiClient = httpClientFactory.CreateClient("ProfileApiClient");
        }

   
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var response = await _profileApiClient.GetAsync($"profile/{userId}");
            if (!response.IsSuccessStatusCode) return View("Error", "Nie udało się pobrać profilu użytkownika.");

            var profile = await response.Content.ReadFromJsonAsync<UserProfileViewModel>();
            return View(profile);
        }

        public async Task<IActionResult> Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var response = await _profileApiClient.GetAsync($"profile/{userId}");
            if (!response.IsSuccessStatusCode) return View("Error", "Nie udało się pobrać profilu użytkownika.");

            var profile = await response.Content.ReadFromJsonAsync<UserProfileViewModel>();
            return View(profile);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _profileApiClient.PutAsJsonAsync("profile", model);
            if (!response.IsSuccessStatusCode) return View("Error", "Nie udało się zaktualizować profilu.");

            return RedirectToAction("Index");
        }
    }
}


