using System.Collections.Generic;
using System.Threading.Tasks;
using coldel.Persistance.Models;
using coldel.Persistance.Models.DTOS;

namespace coldel.Persistance.Core
{
    public interface IHotelRepository
    {
        Task SaveChangesAsync();

        IEnumerable<RoomDTO> GetRooms();
    }
}