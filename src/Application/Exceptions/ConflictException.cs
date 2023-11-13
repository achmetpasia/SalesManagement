namespace Application.Exceptions;

public class ConflictException : ApplicationException
{
    public ConflictException(string message, Exception inner = null) : base(message, inner)
    {
        
    }

    public ConflictException()
    {
        
    }
}
