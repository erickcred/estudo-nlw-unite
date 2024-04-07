using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Domain.Entities;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;

public class RegisterAttendeeOnEventUseCase
{
  private readonly PassInContext _context;

  public RegisterAttendeeOnEventUseCase(PassInContext context)
  {
    _context = context;
  }

  public ResponseRegisterJson Execute(int idEvent, RequestRegisterEventJson request)
  {
    Validation(idEvent, request);

    return InsertAttendee(idEvent, request);
  }

  private void Validation(int idEvent, RequestRegisterEventJson request)
  {
    if (idEvent < 1)
      throw new ErrorValidationException("Um Evento deve ser informado corretamente!");

    var eventEntity = _context.Events.FirstOrDefault(e => e.Id == idEvent);
    var registeredAttendee = _context.Attendees.Any(a => a.EventId == idEvent && a.Email.Equals(request.Email));
    
    if (eventEntity is null)
      throw new NotFoundException("O Evento não pode ser localizado!");

    if (string.IsNullOrWhiteSpace(request.Name))
      throw new ErrorValidationException("O Nome do participante deve ser informado!");

    if (EmailIsValid(request.Email) == false)
      throw new ErrorValidationException("O E-mail do participante deve ser informado!");

    if (registeredAttendee)
      throw new ConflictException($"Participante já cadastrado com o E-mail: {request.Email}!");

    var attendeeTotalforEvent = _context.Attendees.AsNoTracking().Count(a => a.EventId == idEvent);
    if (attendeeTotalforEvent >= eventEntity.MaximumAttendees)
      throw new ErrorValidationException("Evento não tem mais vagas para inscrições!");
  }

  private ResponseRegisterJson InsertAttendee(int idEvent, RequestRegisterEventJson request)
  {
    var resultDb = _context.Attendees.AsNoTracking().FirstOrDefault(a => a.Name == request.Name && a.Email == request.Email);

    using var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
    try
    {
      if (resultDb != null)
        throw new ErrorValidationException($"Participante {resultDb.Name} - {resultDb.Email} já cadastrado!");

      var attendee = new Attendees
      {
        Name = request.Name,
        Email = request.Email,
        EventId = idEvent,
        CreatedAt = DateTime.UtcNow,
      };
      _context.Attendees.Add(attendee);
      _context.SaveChanges();

      transaction.Commit();

      var id = attendee.Id;
      return new ResponseRegisterJson
      {
        Id = id
      };
    }
    catch (Exception ex)
    {
      transaction.Rollback();
      throw;
    }
  }
  
  private bool EmailIsValid(string email)
  {
    try
    {
      new MailAddress(email);
      return true;
    }
    catch (Exception ex)
    {
      return false;
    }
  }
}

