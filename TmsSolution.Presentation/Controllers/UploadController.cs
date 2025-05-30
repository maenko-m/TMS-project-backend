using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TmsSolution.Application.Dtos.Attachment;
using TmsSolution.Application.Interfaces;
using TmsSolution.Domain.Entities;
using TmsSolution.Presentation.Common.Extensions;
using Path = System.IO.Path;

namespace TmsSolution.Presentation.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UploadController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public UploadController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }


        /// <summary>
        /// Uploads an attachment file for a specified project.
        /// </summary>
        /// <param name="dto">
        /// An instance of <see cref="AttachmentCreateDto"/> containing the file and the target project ID.
        /// </param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/>:
        /// <list type="bullet">
        /// <item><description><see cref="OkObjectResult"/> with the uploaded attachment ID if successful.</description></item>
        /// <item><description><see cref="BadRequestObjectResult"/> if the file is missing or invalid.</description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// Authorization is required. The uploaded file is saved in a folder associated with the given ProjectId.
        /// </remarks>
        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] AttachmentCreateDto dto)
        {
            var userId = User.GetUserId();

            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("File does not selected");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", dto.ProjectId.ToString());

            var attachmentId = await _attachmentService.UploadAsync(dto, uploadsFolder, userId);

            return Ok(attachmentId);
        }
    }
}
