using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WebAPISample.Controllers
{
    [RoutePrefix( "api/Values" )]
    public class ValuesController : ApiController
    {
        public object Get()
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            Dictionary<string, string> dic1 = new Dictionary<string, string>()
            {
                {"name", "aaa" },
                {"id", "1"}
            };
            list.Add( dic1 );

            Dictionary<string, string> dic2 = new Dictionary<string, string>()
            {
                {"name", "bbb" },
                {"id", "2"}
            };
            list.Add( dic2 );
            //string jsonStream = "{[name: \"aaa\", id: 1],[name: \"bbb\", id: 2]}";
            string jsonStream = JsonConvert.SerializeObject( list );
            JArray jsonObject = JArray.Parse( jsonStream );
            return jsonObject.ToString();
        }
    }
}
