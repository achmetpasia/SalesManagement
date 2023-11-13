namespace Application.Exceptions;

public class NotFoundException : ApplicationException
{

    public NotFoundException(string message, Exception inner = null) : base(message, inner)
    {
        
    }

    public NotFoundException()
    {
        
    }
}
