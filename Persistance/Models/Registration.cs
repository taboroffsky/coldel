using System;

namespace coldel.Persistance.Models
{
    public class Registration
    {
        public Guid Id { get; set; }

        public Client Client { get; set; }

        public Room Room { get; set; }

        public DateTime CheckInDate { get; set; }

        public int NumberOfDays { get; set; }
    }
}