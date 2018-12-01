using System;
using System.Collections.Generic;
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

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<RegistrationDTO>> Get()
        {
            var registrations = _repository.GetRegistrations();
            return  Ok(registrations);
        }

        //GET api/addsample
        [HttpGet("~/api/addsample")]
        public ActionResult<AddRegistrationResource> GetAddRegistrationResourceSample()
        {
            var sample = new AddRegistrationResource()
            {
                ClientName = "Bob Cobich",
                Phone = "67843298",
                RoomId = new Guid(),
                Start = DateTime.Now,
                End = DateTime.Now
            };

            return Ok(sample);
        }

        [HttpPost]
        public ActionResult<Registration> Add([FromBody] AddRegistrationResource resource)
        {
            var client = _repository.GetOrAddClient(resource.ClientName, resource.Phone);

            var reg = new Registration()
            {
                Id = new Guid(),
                ClientId = client.Id,
                RoomId = resource.RoomId,
                CheckInDate = resource.Start,
                CheckOutDate = resource.End
            };

            _repository.AddRegistation(reg);

            return Ok(reg);
        }
    }
}