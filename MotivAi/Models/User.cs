using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace MotivAi.Models
{
    public class User
    {
        [Key]
        public int User_id { get; set; }

        [Required]
        [MaxLength(100)]
        public  string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        [EmailAddress]
        public  string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Password_hash { get; set; } = string.Empty;

        
        [ForeignKey("Role")]
        public int Role_id { get; set; }

        public DateTime Created_at { get; set; } = DateTime.UtcNow;

        public DateTime? Last_login { get; set; }

        public bool Is_active { get; set; } = true;

        // Navigation properties
        public virtual Role Role { get; set; } = null!;

        public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public virtual ICollection<AI_Request> AI_Requests { get; set; } = new List<AI_Request>();
        public virtual ICollection<Calendar_Event> Calendar_Events { get; set; } = new List<Calendar_Event>();
        public virtual ICollection<Habit> Habits { get; set; } = new List<Habit>();
    }
}