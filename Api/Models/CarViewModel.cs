
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntilopaApi.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Model { get; set; }
        public string RegistrationNr { get; set; }
        public string PicUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int OwnerId { get; set; }
    }
}