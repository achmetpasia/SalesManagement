namespace Application.Exceptions;

public abstract class ApplicationException : Exception
{
    public ApplicationException(string message, Exception inner) : base(message, inner)
    {
        
    }

    public ApplicationException()
    {
        
    }
}
