using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.Rgister;
using PassIn.Communication.Requests;
using System.Text.Json;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
  private readonly ILogger _logger;
  private readonly RegisterEventUseCase _registerEventUseCase;

  public EventsController(
    RegisterEventUseCase registerEventUseCase,
    ILogger logger)
  {
    _registerEventUseCase = registerEventUseCase;
    _logger = logger;
  }

  [HttpPost]
  public IActionResult Register([FromBody] RequestEventJson request)
  {
    try
    {
      var useCase = _registerEventUseCase;
      useCase.Execute(request);
    
      return Created();
    } catch (ArgumentException ex)
    {
      _logger.LogError(JsonSerializer.Serialize(ex));
      return BadRequest(ex.Message);
    } catch (Exception ex)
    {
      _logger.LogError(JsonSerializer.Serialize(ex));
      return null;
    }
  }
}
