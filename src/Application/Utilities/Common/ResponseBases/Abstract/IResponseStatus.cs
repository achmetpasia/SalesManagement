using Newtonsoft.Json;
using System.Net;

namespace Application.Utilities.Common.ResponseBases.Abstract
{
    public interface IResponseStatus
    {
        [JsonProperty("statusCode")]
        public HttpStatusCode StatusCode { get; }
    }
}
