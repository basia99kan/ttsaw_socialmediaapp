using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostAPI.Repositories.Contracts;

namespace PostAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T> : ControllerBase where T : class
    {
        private readonly IGenericRepositoryInterface<T> _genericRepositoryInterface;
        private readonly ILogger<GenericController<T>> _logger;

        public GenericController(IGenericRepositoryInterface<T> genericRepositoryInterface, ILogger<GenericController<T>> logger)
        {
            _genericRepositoryInterface = genericRepositoryInterface;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {

            _logger.LogInformation("Rozpoczęto pobieranie wszystkich elementów typu {EntityType}.", typeof(T).Name);
            try
            {
                var result = await _genericRepositoryInterface.GetAll();
                _logger.LogInformation("Pomyślnie pobrano {Count} elementów typu {EntityType}.", result.Count(), typeof(T).Name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas pobierania elementów typu {EntityType}.", typeof(T).Name);
                return StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd serwera.");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Zgłoszono nieprawidłowe żądanie usunięcia dla {EntityType} z id: {Id}.", typeof(T).Name, id);
                return BadRequest("Invalid request sent");
            }

            _logger.LogInformation("Rozpoczęto usuwanie elementu typu {EntityType} z id: {Id}.", typeof(T).Name, id);
            try
            {
                var result = await _genericRepositoryInterface.DeleteById(id);
                _logger.LogInformation("Pomyślnie usunięto element typu {EntityType} z id: {Id}.", typeof(T).Name, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas usuwania elementu typu {EntityType} z id: {Id}.", typeof(T).Name, id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd serwera.");
            }
        }


        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id<=0) return BadRequest("Invaild request sent");
            return Ok(await _genericRepositoryInterface.GetById(id));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(T model)
        {
            if (model == null)
            {
                _logger.LogWarning("Zgłoszono próbę dodania pustego modelu typu {EntityType}.", typeof(T).Name);
                return BadRequest("Bad request made");
            }

            _logger.LogInformation("Rozpoczęto dodawanie nowego elementu typu {EntityType}.", typeof(T).Name);
            try
            {
                var result = await _genericRepositoryInterface.Insert(model);
                _logger.LogInformation("Pomyślnie dodano element typu {EntityType}.", typeof(T).Name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas dodawania nowego elementu typu {EntityType}.", typeof(T).Name);
                return StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd serwera.");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(T model)
        {
            if (model == null)
            {
                _logger.LogWarning("Zgłoszono aktualizację pustego modelu typu {EntityType}.", typeof(T).Name);
                return BadRequest("Bad request made");
            }

            _logger.LogInformation("Rozpoczęto aktualizację elementu typu {EntityType}.", typeof(T).Name);
            try
            {
                var result = await _genericRepositoryInterface.Update(model);
                _logger.LogInformation("Pomyślnie zaktualizowano element typu {EntityType}.", typeof(T).Name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas aktualizacji elementu typu {EntityType}.", typeof(T).Name);
                return StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd serwera.");
            }
        }

    }
}
