using PrimaryHub.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Angular4API.Attributes;

namespace Angular4API.Controllers
{

    public class SnippetsController : BaseController
    {
        [CustomAuthorize]
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
