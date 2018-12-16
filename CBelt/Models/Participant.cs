using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CBelt.Models;

namespace CBelt.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantId { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }


    }
}
