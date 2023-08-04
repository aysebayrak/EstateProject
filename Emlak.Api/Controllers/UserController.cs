using Emlak.DAL.Abstract;
using Emlak.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Emlak.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("getall")]
        public ActionResult<List<User>> GetAll()
        {
            return _userService.GetAll();   
        }



        [HttpGet("get")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound($"Kullanıcı= {id} yok");
            }

            return user;
        }


        [HttpPost("add")]
        public ActionResult<User> Post([FromBody] User user)
        {
            user.UserID = ObjectId.GenerateNewId().ToString();
            _userService.Create(user);

            return CreatedAtAction(nameof(Get), new { id = user.UserID }, user);
        }

        [HttpPut("update")]
        public ActionResult Put(string id, [FromBody] User user)
        {
            var existingEssate = _userService.GetById(id);
            if (existingEssate == null)
            {
                return NotFound($"Kullanıcı  = {id} yok ");
            }

            _userService.Update(id, user);
            return NoContent();
        }

        [HttpDelete("delete")]
        public ActionResult Delete(string id)
        {
            var essate = _userService.GetById(id);
            if (essate == null)
            {
                 return NotFound($"Kullanıcı  = {id} yok ");
            }

            _userService.Delete(essate.UserID);
            return Ok($"kullanıcı  = {id} silindi");
        }



        [HttpGet("filter")]
        public ActionResult<List<User>> GetByFilter([FromQuery] string? userName = null)
        {
            var user = _userService.GetByFilter(userName);

            if (user.Count == 0)
            {
                return NotFound("Fitreleme yok");
            }
            return Ok(user);
        }


    }
}
