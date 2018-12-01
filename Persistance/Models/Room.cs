using System;

namespace coldel.Persistance.Models
{
    public class Room
    {
        public Guid Id { get; set; }

        public RoomType RoomType { get; set; }

        public int Price { get; set; }

        public int Capacity { get; set; }
    }
}