using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Dtos.Attachment
{
    /// <summary>
    /// Data Transfer Object for uploading an attachment.
    /// </summary>
    public class AttachmentCreateDto
    {
        /// <summary>
        /// The file to be uploaded.
        /// </summary>
        /// <remarks>This field is required. An error will be returned if no file is provided.</remarks>
        [Required(ErrorMessage = "File is required.")]
        public required IFormFile File { get; set; }

        /// <summary>
        /// The ID of the project the attachment is associated with.
        /// </summary>
        /// <remarks>This field is required. An error will be returned if the project ID is missing.</remarks>
        [Required(ErrorMessage = "Project id is required.")]
        public Guid ProjectId { get; set; }
    }
}
