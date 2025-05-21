using System.ComponentModel.DataAnnotations;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Application.Dtos.Project
{
    public class ProjectCreateDto
    {
        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(100, ErrorMessage = "Project name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; } = string.Empty;

        public string? IconPath { get; set; }

        [Required(ErrorMessage = "Access type is required.")]
        [EnumDataType(typeof(ProjectAccessType), ErrorMessage = "Invalid access type.")]
        public ProjectAccessType AccessType { get; set; }

        [Required(ErrorMessage = "Owner ID is required.")]
        public Guid OwnerId { get; set; }

        public List<Guid>? ProjectUserIds { get; set; } = new();
    }
}
