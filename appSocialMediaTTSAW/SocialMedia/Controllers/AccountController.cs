using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using System.Security.Claims;
using System.Net.Http.Json;

namespace SocialMedia.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _identityApiClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _identityApiClient = httpClientFactory.CreateClient("IdentityApiClient");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Email i hasło są wymagane.");
                return View();
            }

            var loginRequest = new { Email = email, Password = password };

            var response = await _identityApiClient.PostAsJsonAsync("/api/authentication/login", loginRequest);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Błąd logowania. Sprawdź dane i spróbuj ponownie.");
                return View();
            }

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (!string.IsNullOrEmpty(loginResponse.Token))
            {
                HttpContext.Session.SetString("UserToken", loginResponse.Token);
            }
            else
            {
                ModelState.AddModelError("", "Login failed. No token received.");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string fullname, string email, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(fullname) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || password != confirmPassword)
            {
                ModelState.AddModelError("", "Uzupełnij wszystkie pola i upewnij się, że hasła są zgodne.");
                return View();
            }

            var registerRequest = new { Fullname = fullname, Email = email, Password = password, ConfirmPassword = confirmPassword };
            var response = await _identityApiClient.PostAsJsonAsync("/api/authentication/register", registerRequest);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Rejestracja nie powiodła się. Spróbuj ponownie.");
                return View();
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        private class LoginResponse
        {
            public string Email { get; set; }
            public string Token { get; set; }
        }
    }
}