using Angular4API.Attributes;
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
    [CustomAuthorize]
    public class GroupsController : BaseController
    {

        [HttpPost]      
        public GetGroupsOut GetGroups([FromBody]GetGroupsIn input)
        {          
            return PrimaryHub.GetGroups(input);
        }

        [HttpPost]   
        public RemoveGroupOut RemoveGroup([FromBody]RemoveGroupIn input)
        {
            return PrimaryHub.RemoveGroup(input);
        }

        [HttpPost]
        public AddGroupOut AddGroup([FromBody]AddGroupIn input)
        {
            return PrimaryHub.AddGroup(input);
        }
    }
}
