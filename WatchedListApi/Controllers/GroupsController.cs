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
    
    public class GroupsController : BaseController
    {

        [HttpPost]
        [CustomAuthorize]
        public GetGroupsOut GetGroups([FromBody]GetGroupsIn input)
        {          
            return PrimaryHub.GetGroups(input);
        }

        [HttpPost]
        [CustomAuthorize]
        public RemoveGroupOut RemoveGroup([FromBody]RemoveGroupIn input)
        {
            return PrimaryHub.RemoveGroup(input);
        }

        [HttpPost]
        [CustomAuthorize]
        public AddGroupOut AddGroup([FromBody]AddGroupIn input)
        {
            return PrimaryHub.AddGroup(input);
        }
    }
}
