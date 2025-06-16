using TmsSolution.Domain.Enums;

namespace TmsSolution.Application.Dtos.Project
{
    public class ProjectOutputDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? IconBase64 { get; set; }
        public ProjectAccessType AccessType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid OwnerId { get; set; }
        public string OwnerFullName { get; set; } = string.Empty;
        public int ProjectUsersCount { get; set; }
        public int TestCasesCount { get; set; }
        public List<Guid>? ProjectUserIds { get; set; }
        public int DefectsCount { get; set; }
    }
}
