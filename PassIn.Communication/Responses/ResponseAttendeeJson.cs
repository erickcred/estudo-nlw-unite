namespace PassIn.Communication.Responses;
public class ResponseAttendeeJson
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime CheckedInAt { get; set; }
}
