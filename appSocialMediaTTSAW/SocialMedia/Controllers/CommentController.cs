using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SocialMedia.Controllers
{
    public class CommentController : Controller
    {
       
            private readonly HttpClient _commentApiClient;

            public CommentController(IHttpClientFactory httpClientFactory)
            {
                _commentApiClient = httpClientFactory.CreateClient("PostApiClient");
            }

            [HttpPost]
            public async Task<IActionResult> AddComment(CommentViewModel model)
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid comment data.");

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Pobieranie UserId użytkownika
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                // Tworzenie obiektu komentarza
                var newComment = new
                {
                    Content = model.Content,
                    PostId = model.PostId,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                };

                // Serializacja danych do JSON-a
                var json = JsonSerializer.Serialize(newComment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Wysyłanie żądania do API
                var response = await _commentApiClient.PostAsync("api/Comment/add", content);

                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest("Failed to add comment.");
                }

                return RedirectToAction("Index", "Post");
            }
       
    }
}
