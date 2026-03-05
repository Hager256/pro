using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotivAi.Models
{
    public class Habit
    {
        [Key]
        public int Habit_id { get; set; }

        [ForeignKey("User")]
        public int User_id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [ForeignKey("Habit_Frequency")]
        public int Frequency_id { get; set; }
        public bool Active { get; set; }

        // Navigation
        public virtual User User { get; set; } 
        public virtual Habit_Frequency Frequency { get; set; } 
    }

}
