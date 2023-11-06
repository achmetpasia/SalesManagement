using Application.Utilities.Common.ResponseBases.Abstract;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using System.Net;

namespace Application.Utilities.Common.ResponseBases.Concrate;

public class ObjectBaseResponse<T> : ResponseBase, IObjectResponse<T> where T : class
{
    protected T _data; 
    public virtual T Data 
    {
        get => _data;
        set => _data = value;
    }

    public ObjectBaseResponse()
    {
        
    }

    public ObjectBaseResponse(T data = null) => Data = data;
    public ObjectBaseResponse(T data, HttpStatusCode statusCode) : this(data) => StatusCode = statusCode;
    public ObjectBaseResponse(HttpStatusCode statusCode, string errorMessage, T data = null) : this(data, statusCode) => Message = errorMessage;
}
