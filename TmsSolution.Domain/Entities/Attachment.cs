using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Domain.Entities
{
    /// <summary>
    /// Файлы-вложения
    /// </summary>
    public class Attachment : ICreatable
    {
        [Key] 
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        [Required, MaxLength(255)]
        public string FileName { get; set; }

        [Required]
        public string FileUrl { get; set; }

        public long FileSize { get; set; }

        [Required, MaxLength(100)]
        public string ContentType { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UploadedById { get; set; }

        [ForeignKey("UploadedById")]
        public User UploadedBy { get; set; }
    }
}
