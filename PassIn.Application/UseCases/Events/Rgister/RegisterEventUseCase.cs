using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Domain.Entities;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Rgister;

public class RegisterEventUseCase
{
  private PassInContext _context;

  public RegisterEventUseCase(PassInContext context)
  {
    _context = context;
  }

  public ResponseRegisterEventJson Execute(RequestEventJson request)
  {
    Validate(request);

    return InsertEvent(request);
  }

  private void Validate(RequestEventJson request)
  {
    if (request.MaximumAttendees < 1)
      throw new ErrorValidationException("O número maximo de participantes é invalido!");

    if (string.IsNullOrWhiteSpace(request.Title))
      throw new ErrorValidationException("O Título é invalido!");

    if (string.IsNullOrWhiteSpace(request.Details))
      throw new ErrorValidationException("O Detalhe é invalido!");
  }

  private ResponseRegisterEventJson InsertEvent(RequestEventJson request)
  {
    using var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
    try
    {
      var newEvent = new Event
      {
        Title = request.Title,
        Details = request.Details,
        Slug = request.Title.ToLower().Replace(" ", "-"),
        MaximumAttendees = request.MaximumAttendees
      };
      _context.Events.Add(newEvent);
      _context.SaveChanges();

      int id = newEvent.Id;

      transaction.Commit();
      return new ResponseRegisterEventJson { Id = id };
    }
    catch
    {
      transaction.Rollback();
      throw;
    }

  }
}


