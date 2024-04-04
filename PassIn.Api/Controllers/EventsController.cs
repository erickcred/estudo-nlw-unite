using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PassIn.Application.UseCases.Events.Rgister;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
  private readonly ILogger<EventsController> _logger;
  private readonly RegisterEventUseCase _registerEventUseCase;

  public EventsController(
    RegisterEventUseCase registerEventUseCase,
    ILogger<EventsController> logger)
  {
    _registerEventUseCase = registerEventUseCase;
    _logger = logger;
  }

  [HttpPost]
  [ProducesResponseType(typeof(ResponseRegistereventJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
  public IActionResult Register([FromBody] RequestEventJson request)
  {
    try
    {
      var useCase = _registerEventUseCase;
      var newEvent = useCase.Execute(request);
    
      return Created(string.Empty, newEvent.Id);
    } catch (ArgumentException ex)
    {
     // _logger.LogError(JsonConvert.SerializeObject(ex));
      return BadRequest(new ResponseErrorJson(ex.Message));
    } catch (Exception ex)
    {
      //_logger.LogError(JsonConvert.SerializeObject(ex));
      return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson(ex.Message));
    }
  }
}
