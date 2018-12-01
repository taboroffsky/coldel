using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coldel.Persistance.Core;
using coldel.Persistance.Models.DTOS;
using coldel.Resources;
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
        public ActionResult<IEnumerable<RoomDTO>> Get()
        {
            var rooms = _repository.GetRooms();
            return  Ok(rooms);
        }

        // POST api/values
        // Returns number of unique pairs combinations (room tuype-capacity) for a specific date.
        [HttpPost]
        public ActionResult<IEnumerable<RoomDTO>> Post([FromBody] GetRoomTypesResource resource)
        {
            var rooms = _repository.GetRoomTypes(resource);

            return Ok(rooms);
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
