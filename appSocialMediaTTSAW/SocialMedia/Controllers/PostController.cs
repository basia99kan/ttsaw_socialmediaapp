using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using System.Security.Claims;

namespace SocialMedia.Controllers
{
  public class PostController : Controller
  {
         private readonly HttpClient _postApiClient;

         public PostController(IHttpClientFactory httpClientFactory)
         {
                _postApiClient = httpClientFactory.CreateClient("PostApiClient");
         }


            
          [HttpGet]
          public async Task<IActionResult> Index()
          {
            var posts = await _postApiClient.GetFromJsonAsync<List<PostViewModel>>("api/Post/all");

            if (posts == null) posts = new List<PostViewModel>();
            return View(posts);
          }

          [HttpGet]
          public async Task<IActionResult> Details(int id)
          {
           if (id <= 0)
                return BadRequest("Invalid post ID.");

          var post = await _postApiClient.GetFromJsonAsync<PostViewModel>($"api/Post/single/{id}");
          if (post == null)
                return NotFound();

          return View(post);
          }

            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Create(PostViewModel model)
            {
                if (!ModelState.IsValid)
                    return View(model);

                var response = await _postApiClient.PostAsJsonAsync("api/Post/add", model);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Failed to create post.");
                    return View(model);
                }

                return RedirectToAction("Index");
            }

            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                if (id <= 0)
                    return BadRequest("Invalid post ID.");

                var post = await _postApiClient.GetFromJsonAsync<PostViewModel>($"api/Post/single/{id}");
                if (post == null)
                    return NotFound();

                return View(post);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(PostViewModel model)
            {
                if (!ModelState.IsValid)
                    return View(model);

                var response = await _postApiClient.PutAsJsonAsync("api/Post/update", model);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Failed to update post.");
                    return View(model);
                }

                return RedirectToAction("Index");
            }

            [HttpPost]
            public async Task<IActionResult> Delete(int id)
            {
                if (id <= 0)
                    return BadRequest("Invalid post ID.");

                var response = await _postApiClient.DeleteAsync($"api/Post/delete/{id}");
                if (!response.IsSuccessStatusCode)
                    return BadRequest("Failed to delete post.");

                return RedirectToAction("Index");
            }

        [HttpGet]
        public async Task<IActionResult> MyPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var response = await _postApiClient.GetAsync($"api/Post/userId={userId}");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Unable to fetch your posts.");
            }

            var posts = await response.Content.ReadFromJsonAsync<List<PostViewModel>>();
            return View(posts ?? new List<PostViewModel>());
        }
    }
    }