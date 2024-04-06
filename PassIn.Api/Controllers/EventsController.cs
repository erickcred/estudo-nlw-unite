using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.Get;
using PassIn.Application.UseCases.Events.Rgister;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
  private readonly ILogger<EventsController> _logger;
  private readonly RegisterEventUseCase _registerEventUseCase;
  private readonly GetEventByIdUseCase _getEvenByIdtsUseCase;

  public EventsController(
    RegisterEventUseCase registerEventUseCase,
    GetEventByIdUseCase getEvenByIdtsUseCase,
    ILogger<EventsController> logger)
  {
    _registerEventUseCase = registerEventUseCase;
    _getEvenByIdtsUseCase = getEvenByIdtsUseCase;
    _logger = logger;
  }

  [HttpPost]
  [ProducesResponseType(typeof(ResponseRegisterEventJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
  public IActionResult Register([FromBody] RequestEventJson request)
  {
    try
    {
      var useCase = _registerEventUseCase;
      var responseEvent = useCase.Execute(request);
    
      return Created(string.Empty, responseEvent);
    } 
    catch (PassInException ex)
    {
      _logger.LogError(ex.ToString());
      return BadRequest(new ResponseErrorJson(ex.Message));
    } 
    catch (Exception ex)
    {
      _logger.LogError(ex.ToString());
      return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson("Erro Deconhecido!"));
    }
  }

  [HttpGet]
  [Route("{id}")]
  [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public IActionResult GetById([FromRoute] int id)
  {
    try
    {
      var useCase = _getEvenByIdtsUseCase;
      var response = useCase.Execute(id);
      return Ok(response);
    }
    catch (PassInException ex)
    {
      _logger.LogError(ex.ToString());
      return BadRequest(new ResponseErrorJson(ex.Message));
    }
    catch (Exception ex)
    {
      _logger.LogError(ex.ToString());
      return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson("Erro Deconhecido!"));
    }
  }
}
