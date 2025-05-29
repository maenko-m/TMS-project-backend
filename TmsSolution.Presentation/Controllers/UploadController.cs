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
    [Route("api")]
    public class UploadController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public UploadController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpPost("/upload")]
        [Authorize]
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
