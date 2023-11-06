namespace Application.Utilities.Common.ResponseBases.Abstract;

public interface IObjectResponse<out T> 
{
    public T Data { get; }
}
