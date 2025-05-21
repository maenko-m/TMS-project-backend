using System.ComponentModel.DataAnnotations;
using TmsSolution.Domain.Enums;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Domain.Entities
{
    public class User : IAuditable
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(10_000)]
        public string IconBase64 { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; } // Role in system

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<Project> OwnedProjects { get; set; } = new();
        public List<ProjectUser> ProjectUsers { get; set; } = new();
        public List<TestCase> CreatedTestCases { get; set; } = new();
        public List<SharedStep> CreatedSharedSteps { get; set; } = new();
        public List<TestRun> AssignedTestRuns { get; set; } = new();
        public List<TestRunTestCase> AssignedTestRunTestCases { get; set; } = new();
        public List<Defect> CreatedDefects { get; set; } = new();
        public List<Attachment> UploadedAttachments { get; set; } = new();
        public List<TestPlan> CreatedTestPlans { get; set; } = new();
        public List<AuditLog> AuditLogs { get; set; } = new();
    }
}
