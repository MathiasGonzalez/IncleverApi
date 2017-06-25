using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace IncleverApi.Handlers
{
    public class ApiKeyHandler : DelegatingHandler
    {
        public string Key { get; set; }

        public string Header { get; set; }

        public ApiKeyHandler(string key, string header)
        {
            Key = key;
            Header = header;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!ValidateKey(request))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }

        private bool ValidateKey(HttpRequestMessage message)
        {
            //var query = message.RequestUri.ParseQueryString();
            //string key = query["key"];
            if (message.Method == HttpMethod.Post)// son todos post
            {

                string key;
                var header = message.Headers.Where(h => h.Key.ToUpper() == Header).FirstOrDefault();

                if (header.Key == null || header.Value == null || header.Value.Count() < 1)
                {

                    throw new Exception($"  el header  : {Header}  tiene valor invalido o nulo : {Key}");
                }
                else
                {
                    key = header.Value.First();
                    return (key == Key);
                }
            }
            return true;
        }
    }
}