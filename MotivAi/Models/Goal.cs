using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotivAi.Models
{
    public class Goal
    {
        [Key]
        public int Goal_id { get; set; }
        public string? Title { get; set; } 
        public string? Description { get; set; }
        public int Status_id { get; set; }
        public int Progress_percentage { get; set; }

        [ForeignKey("User")]
        public int User_id { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }

        // Navigation
        public virtual User User{ get; set; }
        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}