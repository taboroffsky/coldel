using System;

namespace coldel.Persistance.Models.DTOS
{
    public class RoomDTO
    {

        public RoomDTO(Room room)
        {
            Id = room.Id;
            RoomType = room.RoomType.ToString();
            Price = room.Price;
            Capacity = room.Capacity;
        }

         public Guid Id { get; set; }

        public string RoomType { get; set; }

        public int Price { get; set; }

        public int Capacity { get; set; }
    }
}