using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotivAi.Models
{
    public class Notification
    {
        [Key]
        public int Notification_id { get; set; }

        [ForeignKey("User")]
        public int User_id { get; set; }

        [ForeignKey("Notification_Type")]
        public int Type_id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Created_at { get; set; }
        public bool Read { get; set; }

        // Navigation
        public virtual User User { get; set; } 
        public virtual Notification_Type Type { get; set; }
    }

}
