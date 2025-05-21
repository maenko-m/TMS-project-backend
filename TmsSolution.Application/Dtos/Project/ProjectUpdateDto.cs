using System.ComponentModel.DataAnnotations;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Application.Dtos.Project
{
    public class ProjectUpdateDto
    {
        [StringLength(100, ErrorMessage = "Project name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }
        public string? IconPath { get; set; }

        [EnumDataType(typeof(ProjectAccessType), ErrorMessage = "Invalid access type.")]
        public ProjectAccessType? AccessType { get; set; }
        public Guid? OwnerId { get; set; }

        public List<Guid>? ProjectUserIds { get; set; } = new();
    }
}
