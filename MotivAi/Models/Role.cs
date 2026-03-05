using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MotivAi.Models
{
    public class Role
    {

        [Key]
        public int Role_id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}