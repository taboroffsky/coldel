using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using coldel.Persistance.Models;
using coldel.Persistance.Models.DTOS;
using coldel.Resources;

namespace coldel.Persistance.Core
{
    public interface IHotelRepository
    {
        void SaveChanges();

        Room GetRoom(Guid id);

        IEnumerable<RoomDTO> GetRooms();

        IEnumerable<RoomDTO> GetRoomTypes(GetRoomTypesResource resource);

        Registration GetRegistration(Guid id);

        IEnumerable<RegistrationDTO> GetRegistrations();

        Client GetOrAddClient(string clientName, string phone);

        void AddRegistation(Registration registration);
    }
}