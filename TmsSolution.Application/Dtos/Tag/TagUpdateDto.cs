using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Dtos.Tag
{
    public class TagUpdateDto
    {
        [StringLength(100, ErrorMessage = "Tag name cannot exceed 100 characters.")]
        public string? Name { get; set; }
    }
}
