using Emlak.DAL.Abstract;
using Emlak.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Emlak.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstateController : ControllerBase
    {
        private readonly IEstateService _estateService;

        public EstateController(IEstateService estateService)
        {
            _estateService = estateService;
        }


        [HttpGet("getall")]
        public ActionResult<List<Estate>> Get()
        {
            return _estateService.GetAll();
        }

        [HttpGet("get")]
        public ActionResult<Estate> Get(string id)
        {
            var essate = _estateService.GetById(id);
            if (essate == null)
            {
                return NotFound($"Aranan emlak = {id} bulnamadı");
            }

            return essate;
        }

        [HttpPost("add")]
        public ActionResult<Estate> Post([FromBody] Estate estate)
        {
            estate.EstateID = ObjectId.GenerateNewId().ToString();
            _estateService.Create(estate);

            return CreatedAtAction(nameof(Get), new { id = estate.EstateID }, estate);
        }


        [HttpPut("update")]
        public ActionResult Put(string id, [FromBody] Estate estate)
        {
            var existingEssate = _estateService.GetById(id);
            if (existingEssate == null)
            {
                return NotFound($"Aranan emlak = {id} bulunamadı");
            }

            _estateService.Update(id, estate);
            return NoContent();
        }

        [HttpDelete("delete")]
        public ActionResult Delete(string id)
        {
            var essate = _estateService.GetById(id);
            if (essate == null)
            {
                return NotFound($"Emlak  = {id} bulunamadı");
            }


            _estateService.Delete(id);
            return Ok($"Emlak bu  = {id} bulunamadı");
        }





        [HttpGet("filter")]
        public ActionResult<List<Estate>> GetByFilter([FromQuery] string? city = null, [FromQuery] string? type = null,
            [FromQuery] int? room = null, [FromQuery] string? title = null, [FromQuery] int? price = null, [FromQuery] string? buildYear = null)
        {
            var estate = _estateService.GetByFilter(city, type, room, title, price, buildYear);

            if (estate.Count == 0)
            {
                return NotFound("Uygun filitreleme değil");
            }
            return Ok(estate);
        }
    }

}

