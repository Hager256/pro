using System.ComponentModel.DataAnnotations;

namespace MotivAi.Models
{
    public class Notification_Type
    {
        [Key]
        public int Type_id { get; set; }
        public string Type_name { get; set; } = string.Empty;

        // Navigation
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }

}
