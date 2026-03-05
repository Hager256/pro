using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotivAi.Models
{
    public class Calendar_Event
    {
        [Key]
        public int Event_id { get; set; }

        [ForeignKey("User")]
        public int User_id { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Start_at { get; set; }
        public DateTime End_at { get; set; }

        // Navigation
        public virtual User User{ get; set; }
    }

}
