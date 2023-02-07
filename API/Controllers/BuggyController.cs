using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API;
using API.Data;
using Microsoft.AspNetCore.Authorization;
using API.Entities;

namespace API.Controllers
{

    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var user = _context.Users.Find(-1);
            if (user == null)
            {
                return NotFound();
            }
            else

                return user;
        }
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
               var user = _context.Users.Find(-1);
                var userreturn = user.ToString();
                return userreturn;
            // try
            // {
            //     var user = _context.Users.Find(-1);
            //     var userreturn = user.ToString();
            //     return userreturn;
            // }
            // catch (Exception ex)
            // {
            //     return StatusCode(500, "internal error 500");
            // }
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("this is bad request");
        }
    }
}
