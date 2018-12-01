using System;

namespace coldel.Resources
{
    public class AddRegistrationResource
    {
        public string ClientName { get; set; }

        public string Phone { get; set; }

        public Guid RoomId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}