using System.ComponentModel.DataAnnotations.Schema;

namespace PassIn.Domain.Entities;

public class Event
{
  public int Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Details { get; set; } = string.Empty;
  public string Slug { get; set; } = string.Empty;
  public int MaximumAttendees { get; set; }
}
