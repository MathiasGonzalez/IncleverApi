using PrimaryHub.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Angular4API.Controllers
{
    public class ProfesionalController : BaseController
    {

        [HttpPost]
        public AddProfesionalOut AddProfesional([FromBody]AddProfesionalIn input)
        {
            return PrimaryHub.AddProfesional(input);
        }

    }
}