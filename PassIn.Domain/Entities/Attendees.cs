namespace PassIn.Domain.Entities;

public class Attendees
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public int EventId { get; set; }
  public DateTime CreatedAt { get; set; }
}
