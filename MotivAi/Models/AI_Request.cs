using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotivAi.Models
{
    public class AI_Request
    {
        [Key]
        public int Request_id { get; set; }

        [ForeignKey("User")]
        public int User_id { get; set; }
        public string Prompt { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public DateTime Created_at { get; set; }
        public DateTime Response_time { get; set; }
        public string Status { get; set; } = "Pending";

        // Navigation
        public virtual User  User { get; set; } 
    }

}
