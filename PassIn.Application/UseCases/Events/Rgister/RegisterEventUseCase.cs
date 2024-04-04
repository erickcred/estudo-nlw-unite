using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Domain.Entities;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System.Transactions;

namespace PassIn.Application.UseCases.Events.Rgister;

public class RegisterEventUseCase
{
  private PassInContext _context;

  public RegisterEventUseCase(PassInContext context)
  {
    _context = context;
  }

  public ResponseEventJson Execute(RequestEventJson request)
  {
    Validate(request);

    var newEvent = new Event
    {
      Id = 0,
      Title = request.Title,
      Details = request.Details,
      Slug = request.Title.ToLower().Replace(" ", "-"),
      MaximumAttendees = request.MaximumAttendees
    };
    _context.Events.Add(newEvent);
    _context.SaveChanges();
    return new ResponseEventJson
    {
      Title = newEvent.Title,
      Details = newEvent.Details,
      MaximumAttendees = newEvent.MaximumAttendees
    };

  }

  private void Validate(RequestEventJson request)
  {
    if (request.MaximumAttendees < 1)
      throw new PassInException("O número maximo de participantes é invalido!");

    if (string.IsNullOrWhiteSpace(request.Title))
      throw new PassInException("O Título é invalido!");

    if (string.IsNullOrWhiteSpace(request.Details))
      throw new PassInException("O Detalhe é invalido!");
  }
}


