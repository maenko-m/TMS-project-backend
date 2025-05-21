using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using TmsSolution.Domain.Enums;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Domain.Entities
{
    public class Project : IAuditable
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(10_000)]
        public string IconBase64 { get; set; } = string.Empty;

        [Required]
        public ProjectAccessType AccessType { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        public List<ProjectUser> ProjectUsers { get; set; } = new();
        public List<TestCase> TestCases { get; set; } = new();
        public List<TestSuite> TestSuites { get; set; } = new();
        public List<TestRun> TestRuns { get; set; } = new();
        public List<SharedStep> SharedSteps { get; set; } = new();
        public List<Defect> Defects { get; set; } = new();
        public List<Attachment> Attachments { get; set; } = new();
        public List<TestPlan> TestPlans { get; set; } = new();
        public List<Milestone> Milestones { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
        public List<AuditLog> AuditLogs { get; set; } = new();

        /// <summary>
        /// Проверяет, имеет ли пользователь доступ к проекту
        /// </summary>
        public bool HasAccess(User user)
        {
            if (AccessType == ProjectAccessType.Public)
                return true;

            return user != null && ProjectUsers.Any(u => u.Id == user.Id);
        }
        
    }
}
