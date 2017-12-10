using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json.Linq;

namespace WebAPISample.Controllers
{
    [RoutePrefix("api/Command")]
    public class CommandController : ApiController
    {
        public IHttpActionResult Post( JObject bodyObject )
        {
            return Ok();
        }
    }
}
