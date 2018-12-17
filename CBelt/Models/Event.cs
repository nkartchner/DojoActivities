using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CBelt.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }


        [Required]
        [MinLength(2, ErrorMessage = "Title must be at least 2 characters in length.")]
        public string Title { get; set; }


        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public TimeSpan Time { get; set; } = DateTime.Now.TimeOfDay;


        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Duration { get; set; }


        [Required]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters in length.")]
        public string Description { get; set; }


        public int UserId { get; set; }
        public string CreatedBy { get; set; }
        public User User { get; set; }


        public List<Participant> Participants { get; set; }


        public string DurationToString()
        { 
            if (this.Duration.Days != 0)
            {
                if (this.Duration.Days > 1)
                {
                    return String.Format("{0:d} Days", this.Duration.Days);
                }
                return String.Format("{0:d} Day", this.Duration.Days);
            }
            if (this.Duration.Hours != 0)
            {
                if (this.Duration.Hours > 1)
                {
                    return String.Format("{0:d} Hours" , this.Duration.Hours);
                }
                return String.Format("{0:d} Hour", this.Duration.Hours);
            }
            if (this.Duration.Minutes != 0)
            {
                if (this.Duration.Minutes > 1)
                {
                    return String.Format("{0:d} Minutes", this.Duration.Minutes);
                }
                return String.Format("{0:d} Minute", this.Duration.Minutes);
            }
            return "Invalid Duration Entry. Please Re-create the event with a valid duration entry!";
        }
        


    }
}
