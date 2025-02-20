using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IdentityAPI.Repositories.Contracts;
using IdentityAPI.DTOs;

namespace IdentityAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserAccount _userAccountI;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IUserAccount userAccount, ILogger<AuthenticationController> logger)
        {
            this._userAccountI = userAccount;
            _logger = logger;

        }
        [HttpPost("register")]
        public async Task<IActionResult> CreateAsync(Register user)
        {

            if (user == null)
            {
                _logger.LogWarning("Próba dodania pustego modelu rejestracji.");
                return BadRequest("Model jest pusty.");
            }

            _logger.LogInformation("Rozpoczęto rejestrację użytkownika.");
            try
            {
                var result = await _userAccountI.CreateAsync(user);
                _logger.LogInformation("Pomyślnie zarejestrowano użytkownika.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas rejestracji użytkownika.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd serwera.");
            }
        }

        [HttpPost("login")]

        public async Task<IActionResult> SignInAsync(Login user)
        {
            if (user == null)
            {
                _logger.LogWarning("Próba logowania z pustym modelem.");
                return BadRequest("Model jest pusty.");
            }

            _logger.LogInformation("Rozpoczęto logowanie użytkownika.");
            try
            {
                var result = await _userAccountI.SignInAsync(user);
                _logger.LogInformation("Pomyślnie zalogowano użytkownika.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas logowania użytkownika.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd serwera.");
            }

        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshToken token)
        {
            if (token == null)
            {
                _logger.LogWarning("Próba odświeżenia tokenu z pustym modelem.");
                return BadRequest("Model jest pusty.");
            }

            _logger.LogInformation("Rozpoczęto odświeżanie tokenu.");
            try
            {
                var result = await _userAccountI.RefreshTokenAsync(token);
                _logger.LogInformation("Pomyślnie odświeżono token.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas odświeżania tokenu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd serwera.");
            }
        }

        

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var users = await _userAccountI.GetRoles();
            if (users == null) return NotFound();
            return Ok(users);
        }


    }
}
