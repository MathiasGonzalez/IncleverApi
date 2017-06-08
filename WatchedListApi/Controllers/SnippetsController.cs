using PrimaryHub.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Angular4API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SnippetsController : BaseController
    {
        [HttpPost]
        public FirstSnippetsOut FirstSnippets([FromBody]FirstSnippetsIn input)
        {
            return PrimaryHub.FirstSnippets(input);
        }

        [HttpPost]
        public AddSnippetOut AddSnippet([FromBody]AddSnippetIn input)
        {
            return PrimaryHub.AddSnippet(input);
        }

        [HttpPost]
        public AddSnippetOut EditSnippet([FromBody]AddSnippetIn input)
        {
            return PrimaryHub.EditSnippet(input);
        }
    }
}
