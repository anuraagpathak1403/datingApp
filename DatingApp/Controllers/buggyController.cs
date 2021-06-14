using DatingApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    public class buggyController : baseApiController
    {
        public readonly dataContext _context;
        public buggyController(dataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> getSecret()
        {
            return "secret  text";
        }

        [HttpGet("not-found")]
        public ActionResult<string> getNotFound()
        {
            var thing = _context.appUsers.Find(-1);
            if (thing == null) return NotFound();
            return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<string> getServerError()
        {
            var thing = _context.appUsers.Find(-1);
            var thingToReturn = thing.ToString();
            return Ok(thingToReturn);
        }

        [HttpGet("bad-request")]
        public ActionResult<string> getBadRequest()
        {
            return BadRequest("This is not a good request.");
        }
    }
}
