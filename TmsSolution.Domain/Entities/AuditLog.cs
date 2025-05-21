using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Domain.Entities
{
    public class AuditLog : ICreatable
    {
        [Key] 
        public Guid Id { get; set; }

        public Guid? ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public string EntityType { get; set; }

        public Guid EntityId { get; set; }

        public string Details { get; set; } // JSON

        public DateTime CreatedAt { get; set; }
    }
}
