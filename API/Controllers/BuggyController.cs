using API.Errors;
using Infrastuctre.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
             _context = context;
        }
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            var thing = _context.Products.Find(90);
            if (thing == null)
            {
                return NotFound(new ApiResponce(404));
            }
            return Ok(thing);
        }

        [HttpGet("servererror")] 
        public IActionResult GetServerError()
        {
            var thing = _context.Products.Find(80);
            var thingtoreturn = thing.ToString();
            return Ok();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponce(400));
        }
        [HttpGet("notfound/{id}")]
        public IActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
