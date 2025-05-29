using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Attachment;

namespace TmsSolution.Application.Dtos.TestStep
{
    public class TestStepOutputDto
    {
        public Guid Id { get; set; }
        public Guid TestCaseId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ExpectedResult { get; set; }
        public int Position { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //public List<AttachmentOutputDto> Attachments { get; set; } = new();
    }
}
