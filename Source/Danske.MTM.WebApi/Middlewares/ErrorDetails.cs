using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Danske.MTM.WebApi.Middlewares
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }

        public ErrorDetails()
        {
            Errors = new Dictionary<string, string[]>();
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
