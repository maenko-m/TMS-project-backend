using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TmsSolution.Domain.Enums;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Domain.Entities
{
    public class ProjectUser : ICreatable
    {
        [Key] 
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public UserRole Role { get; set; } // Role in project

        public DateTime CreatedAt { get; set; }
    }
}
