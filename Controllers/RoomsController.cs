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

        // POST api/rooms
        // Returns number of unique pairs combinations (room type-capacity) for a specific date.
        [HttpPost]
        public ActionResult<IEnumerable<RoomDTO>> Post([FromBody] GetRoomTypesResource resource)
        {
            var rooms = _repository.GetRoomTypes(resource);

            return Ok(rooms);
        }

        [HttpPut]
        public ActionResult InPut([FromBody] Temp temp)
        {
            var t = temp.Text;

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Temp {
        public string Text { get; set; }
    }
}
