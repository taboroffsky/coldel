using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coldel.Persistance.Core;
using coldel.Persistance.Models;
using Microsoft.AspNetCore.Mvc;

namespace coldel.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomsControlller : ControllerBase
    {
        private readonly IHotelRepository _repository;

        public RoomsControlller(IHotelRepository repository)
        {
            _repository = repository;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Room>> Get()
        {
            var rooms = _repository.GetRooms();
            return  Ok(rooms);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return Ok("val");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
