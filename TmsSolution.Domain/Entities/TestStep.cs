using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Domain.Entities
{
    public class TestStep : IAuditable
    {
        [Key] 
        public Guid Id { get; set; }

        public Guid TestCaseId { get; set; }

        [ForeignKey("TestCaseId")]
        public TestCase TestCase { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? ExpectedResult { get; set; }

        public int Position { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<Attachment> Attachments { get; set; } = new();
    }
}
