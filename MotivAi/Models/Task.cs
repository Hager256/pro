using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotivAi.Models
{
    public class Task
    {
        [Key]
        public int Task_id { get; set; }

        [ForeignKey("Goal")]
        public int Goal_id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        [ForeignKey("Status")]
        public int Status_id { get; set; }
        public int Priority { get; set; }
        public bool Completion_status { get; set; }

        // Navigation
        public virtual Goal Goal { get; set; }
        public virtual Task_Status Status { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
