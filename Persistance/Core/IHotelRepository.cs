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
        Task SaveChangesAsync();

        IEnumerable<RoomDTO> GetRooms();

        IEnumerable<RoomDTO> GetRoomTypes(GetRoomTypesResource resource);

        IEnumerable<RegistrationDTO> GetRegistrations();

        Client GetOrAddClient(string clientName, string phone);

        void AddRegistation(Registration registration);
    }
}