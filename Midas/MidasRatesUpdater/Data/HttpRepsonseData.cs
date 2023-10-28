using System.Net;

namespace MidasRatesUpdater.Data
{
    public class HttpRepsonseData
    {
        public bool Success { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string? ReasonPhrase { get; set; }

        public string Content { get; set; }
    }
}
