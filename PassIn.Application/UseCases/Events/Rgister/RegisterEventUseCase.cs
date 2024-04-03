using Microsoft.Extensions.Logging;
using PassIn.Communication.Requests;

namespace PassIn.Application.UseCases.Events.Rgister;

public class RegisterEventUseCase
{
  private readonly ILogger _logger;

  public RegisterEventUseCase(ILogger logger)
  {
    _logger = logger;
  }

  public void Execute(RequestEventJson request)
  {
    Validate(request);
  }

  private void Validate(RequestEventJson request)
  {
    if (request.MaximumAttendees < 1)
      throw new ArgumentException("O número maximo de participantes é invalido!");

    if (string.IsNullOrWhiteSpace(request.Title))
      throw new ArgumentException("O Título é invalido!");

    if (string.IsNullOrWhiteSpace(request.Details))
      throw new ArgumentException("O Detalhe é invalido!");
  }
}


