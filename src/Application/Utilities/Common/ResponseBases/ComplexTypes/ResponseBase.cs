using Application.Utilities.Common.ResponseBases.Abstract;
using System.Net;
using System.Text.Json.Serialization;

namespace Application.Utilities.Common.ResponseBases.ComplexTypes;

public class ResponseBase : IResponseMessage, IResponseStatus
{
    public string Message {  get; set; }

    [JsonIgnore]
    public HttpStatusCode StatusCode {  get; set; }
}
