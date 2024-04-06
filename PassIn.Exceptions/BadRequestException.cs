namespace PassIn.Exceptions;

public class ErrorValidationException : PassInException
{
  public ErrorValidationException(string message) : base(message)
  { }
}
