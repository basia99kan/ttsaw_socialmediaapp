using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using System.Diagnostics;

namespace SocialMedia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _postApiClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _postApiClient = httpClientFactory.CreateClient("PostApiClient");
        }


        public async Task<IActionResult> Index()
        {
            var posts = await _postApiClient.GetFromJsonAsync<List<PostViewModel>>("api/Post/all");

            posts ??= new List<PostViewModel>();
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}