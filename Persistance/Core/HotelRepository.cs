using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coldel.Persistance.Models;
using coldel.Persistance.Models.DTOS;
using coldel.Resources;
using Microsoft.EntityFrameworkCore;

namespace coldel.Persistance.Core
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelDbContext _context;
        public HotelRepository(HotelDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddRegistation(Registration registration)
        {
            _context.Registrations.Add(registration);
            _context.SaveChanges();
        }

        public Client GetOrAddClient(string clientName, string phone)
        {
            Client client;
            client = _context.Clients.FirstOrDefault(cl => cl.Name == clientName && cl.Phone == cl.Phone);

            if (client != null)
            {
                return client;
            }

            client = new Client()
            {
                Id = new Guid(),
                Name = clientName,
                Phone = phone
            };

            _context.Add(client);
            _context.SaveChanges();

            return client;            
        }

        public IEnumerable<RegistrationDTO> GetRegistrations()
        {
            return _context.Registrations.Include(reg => reg.Client)
                                         .Include(reg => reg.Room)
                                         .Select(reg => new RegistrationDTO(reg))
                                         .ToList();
        }

        public IEnumerable<RoomDTO> GetRoomTypes(GetRoomTypesResource resource)
        {
            var roomsNotAvailable = _context.Registrations.Where(r => (r.CheckInDate > resource.Start && r.CheckInDate < resource.End)
                                                                   || (r.CheckOutDate > resource.Start && r.CheckOutDate < resource.End))
                                                          .Select(r => r.RoomId).ToList();

            var remainingRooms = GetRooms().Where(r => roomsNotAvailable.All(nar => nar != r.Id)).ToList();

            var dictionary = new Dictionary<string, RoomDTO>();

            remainingRooms.ForEach(r => dictionary.TryAdd($"{r.RoomType}.{r.Capacity}", r));

            return dictionary.Values.OrderBy(r => r.Capacity).ToList();
        }

        public IEnumerable<RoomDTO> GetRooms()
        {
            return _context.Rooms.Select(r => new RoomDTO(r)).ToList();
        }

        public void SaveChanges()
        {
             _context.SaveChanges();
        }

        public Registration GetRegistration(Guid id)
        {
            return _context.Registrations.Find(id);
        }

        public Room GetRoom(Guid id)
        {
            return _context.Rooms.Find(id);
        }
    }
}