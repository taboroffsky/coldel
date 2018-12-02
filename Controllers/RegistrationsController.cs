using System;
using System.Collections.Generic;
using System.Linq;
using coldel.Persistance.Core;
using coldel.Persistance.Models;
using coldel.Persistance.Models.DTOS;
using coldel.Resources;
using Microsoft.AspNetCore.Mvc;

namespace coldel.Controllers
{
    [Route("api/registrations")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly IHotelRepository _repository;

        public RegistrationsController(IHotelRepository repository)
        {
            _repository = repository;
        }

        // GET api/registrations
        [HttpGet]
        public ActionResult<IEnumerable<RegistrationDTO>> Get()
        {
            var registrations = _repository.GetRegistrations();
            return  Ok(registrations);
        }

        // GET api/addsample
        [HttpGet("~/api/addsample")]
        public ActionResult<AddRegistrationResource> GetAddRegistrationResourceSample()
        {
            var sample = new AddRegistrationResource()
            {
                ClientName = "Bob Cobich",
                Phone = "67843298",
                RoomId = new Guid(),
                RegistrationId = new Guid(),
                Start = DateTime.Now,
                End = DateTime.Now
            };

            return Ok(sample);
        }

        // POST /api/registrations
        [HttpPost]
        public ActionResult<RegistrationDTO> Add([FromBody] AddRegistrationResource resource)
        {
            var client = _repository.GetOrAddClient(resource.ClientName, resource.Phone);
            var room = _repository.GetRoom(resource.RoomId);

            var reg = new Registration()
            {
                Id = new Guid(),
                ClientId = client.Id,
                Client = client,
                RoomId = room.Id,
                Room = room,
                CheckInDate = resource.Start,
                CheckOutDate = resource.End
            };

            _repository.AddRegistation(reg);

            return Ok(new RegistrationDTO(reg));
        }


        // PUT /api/registrations
        [HttpPut]
        public ActionResult Edit([FromBody] AddRegistrationResource resource)
        {
            var reg = _repository.GetRegistration(resource.RegistrationId);

            var client = _repository.GetOrAddClient(resource.ClientName, resource.Phone);

            reg.ClientId = client.Id;
            reg.RoomId = resource.RoomId;
            reg.CheckInDate = resource.Start;
            reg.CheckOutDate = resource.End;

            _repository.SaveChanges();

            return Ok();
        }

        // DELETE /api/registrations
        [HttpDelete]
        public ActionResult Delete([FromBody] DeleteRegistrationResource resource)
        {
            _repository.DeleteRegistration(resource.Id);

            return Ok();
        }
    }
}