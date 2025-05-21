using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Domain.Entities
{
    public class Tag : ICreatable
    {
        [Key] 
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
