using TmsSolution.Domain.Enums;

namespace TmsSolution.Application.Dtos.User
{
    public class UserOutputDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? IconBase64 { get; set; }
        public UserRole Role { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
