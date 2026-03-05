using System.ComponentModel.DataAnnotations;

namespace MotivAi.Models
{
    public class Habit_Frequency
    {
        [Key]
        public int Frequency_id { get; set; }
        public string Frequency_name { get; set; } = string.Empty;

        // Navigation
        public ICollection<Habit> Habits { get; set; } = new List<Habit>();
    }

}
