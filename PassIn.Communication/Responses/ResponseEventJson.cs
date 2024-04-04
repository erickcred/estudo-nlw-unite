using System.ComponentModel.DataAnnotations.Schema;

namespace PassIn.Communication.Responses;

public class ResponseEventJson
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public int MaximumAttendees { get; set; }
    public int AttendeesAmount { get; set; }
}
