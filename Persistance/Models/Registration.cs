using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coldel.Persistance.Models
{
    public class Registration
    {
        public Guid Id { get; set; }

        public Client Client { get; set; }

        public Guid ClientId { get; set; }

        public Room Room { get; set; }

        public Guid RoomId { get; set; }

        [Column(TypeName="date")]
        public DateTime CheckInDate { get; set; }

        [Column(TypeName="date")]
        public DateTime CheckOutDate { get; set; }
    }
}