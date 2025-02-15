using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIDentify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IUserRepositry user1;
        public AuthController(IUserRepositry user1) {
            this.user1 = user1;
        }
        
        [HttpGet]
        public IActionResult Test()
        {
            var tst = user1.GetAll();
            return Ok(tst);
        }
        [HttpPost]
        public IActionResult Post(User user)
        {
            if (ModelState.IsValid)
            {
                user1.Add(user);
                string url = Url.Link("ProductDetails", new { id = user.UserID });
                return Created(url, user);

            }
            return BadRequest(ModelState);

           
        }
    }
}
