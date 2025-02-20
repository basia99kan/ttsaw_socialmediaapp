using PostAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostAPI.Repositories.Contracts;

namespace PostAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : GenericController<Comment>
    {
        private readonly IGenericRepositoryInterface<Comment> _genericRepositoryInterface;
        private readonly ILogger<CommentController> _logger;
        public CommentController(IGenericRepositoryInterface<Comment> genericRepositoryInterface, ILogger<CommentController> logger)
        : base(genericRepositoryInterface, logger)
        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _logger = logger;

        }
    }
}
