using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL;
using Microsoft.AspNetCore.Mvc;

namespace DungeonDwellerRest.Controllers {
    [Route("/api/generate")]
    public class DungeonGeneratorController : Controller {

        [HttpGet]
        public JsonResult GetMap()
        {
            return Json(DungeonGenerator.Generate(null));
        }        
    }
}
