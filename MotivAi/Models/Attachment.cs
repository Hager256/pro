using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotivAi.Models
{
    public class Attachment
    {
        [Key]
        public int Attachment_id { get; set; }
        public string File_name { get; set; } = string.Empty;
        public string Mime_type { get; set; } = string.Empty;
        public long Size { get; set; }
        public string Url { get; set; } = string.Empty;
        public DateTime Uploaded_at { get; set; }

        [ForeignKey("Task")]
        public int Task_id { get; set; }

        // Navigation
        public virtual Task Task  { get; set; } 
    }

}
