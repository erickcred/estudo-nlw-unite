using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Get;

public class GetEventByIdUseCase
{

  private readonly PassInContext _context;

  public GetEventByIdUseCase(PassInContext context)
  {
    _context = context;
  }

  public ResponseEventJson Execute(int id)
  {
    return ReturneEvet(id);
  }

  private ResponseEventJson ReturneEvet(int id)
  {
    var dataBase = _context.Events.AsNoTracking().FirstOrDefault(e => e.Id == id);
    if (dataBase is null)
      throw new NotFoundException($"Não foi possível localizar esse evento! id: {id}");

    var responseEvent = new ResponseEventJson
    {
      Id = dataBase.Id,
      Title = dataBase.Title,
      Details = dataBase.Details,
      AttendeesAmount = 0,
      MaximumAttendees = dataBase.MaximumAttendees
    };

    return responseEvent;
  }
}
