using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Dtos.Attachment
{
    public class AttachmentCreateDto
    {
        [Required(ErrorMessage = "File is required.")]
        public required IFormFile File { get; set; }

        [Required(ErrorMessage = "Project id is required.")]
        public Guid ProjectId { get; set; }
    }
}
