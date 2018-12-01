using System;

namespace coldel.Persistance.Models.DTOS
{
    public class RegistrationDTO
    {

        public RegistrationDTO(Registration regisration)
        {
            Id = regisration.Id;
            Client = regisration.Client;
            Room = new RoomDTO(regisration.Room);
            Start = regisration.CheckInDate;
            End = regisration.CheckOutDate;
            Title = $"{Client.Name} ({Client.Phone})";
        }

        public Guid Id { get; set; }

        public Client Client { get; set; }

        public RoomDTO Room { get; set; }
        
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Title { get; set; }
    }
}