using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using zp = zipTools;
namespace IncleverApi.Handlers
{

    public class QueryStringHandler : DelegatingHandler
    {


        public QueryStringHandler()
        {


        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var decompress = request.Headers.FirstOrDefault(x => x.Key == "compress");
            bool isCompressed = decompress.Key != null && decompress.Value.FirstOrDefault() == "true";

            if (request.Method.Method == "GET")
            {
                if (request.RequestUri.Query == null || request.RequestUri.Query == string.Empty)
                {
                    // request.RequestUri = new Uri(request.RequestUri.AbsoluteUri + "?description=completadoEnHandler");
                }
                else
                {
                    if (isCompressed)
                    {
                        string queryRequest = request.RequestUri.Query.ToString();
                        queryRequest = queryRequest.Substring(1, queryRequest.Length - 2);
                        request.Properties.Add("DATA", zp.zipTools.Restore(queryRequest));
                    }
                }
            }
            if (request.Method.Method == "POST")
            {
                if (isCompressed)
                {
                    string queryRequest = request.RequestUri.Query.ToString();
                    queryRequest = queryRequest.Substring(1, queryRequest.Length - 2);
                    request.Properties.Add("DATA", zp.zipTools.Restore(queryRequest));

                }
                else
                {
                    //request.Properties.Add("DATA", zp.zipTools.Restore(request.Content.ReadAsStringAsync().Result));

                }
            }
            return base.SendAsync(request, cancellationToken);
        }
    }

}