using PostAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostAPI.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace PostAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]

    public class PostController : GenericController<Post>
    {
        private readonly IGenericRepositoryInterface<Post> _genericRepositoryInterface;
        private readonly ILogger<PostController> _logger;


        public PostController(IGenericRepositoryInterface<Post> genericRepositoryInterface,
            ILogger<PostController> logger)
            : base(genericRepositoryInterface, logger)
        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _logger = logger;
        }
    }

}
