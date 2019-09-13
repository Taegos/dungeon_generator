using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DLL;

namespace REST.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class GenerateController : ApiController
    {
        // GET: api/Generate
        public HttpResponseMessage Get() {
            bool [,] dungeon = DungeonGenerator.Generate(null);
            return Request.CreateResponse(HttpStatusCode.OK, dungeon);
        }
    }
}
