using System.ComponentModel.DataAnnotations;

namespace MotivAi.Models
{
    public class Task_Status
    {

        [Key]
        public int Status_id { get; set; }
        public string Status_name { get; set; } = string.Empty;

        // Navigation
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
