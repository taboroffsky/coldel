using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coldel.Persistance.Models;
using coldel.Persistance.Models.DTOS;

namespace coldel.Persistance.Core
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelDbContext _context;
        public HotelRepository(HotelDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<RoomDTO> GetRooms()
        {
            return _context.Rooms.Select(r => new RoomDTO(r)).ToList();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}