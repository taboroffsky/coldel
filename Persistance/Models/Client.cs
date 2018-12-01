using System;
using System.ComponentModel.DataAnnotations;

namespace coldel.Persistance.Models
{
    public class Client
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }
    }
}