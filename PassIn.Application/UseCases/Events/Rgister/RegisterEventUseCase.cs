using PassIn.Communication.Requests;

namespace PassIn.Application.UseCases.Events.Rgister;

public class RegisterEventUseCase
{

  public RegisterEventUseCase()
  {
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


