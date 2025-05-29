using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;
using TmsSolution.Application.Dtos.Attachment;

namespace TmsSolution.Application.Dtos.Defect
{
    public class DefectOutputDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? TestRunId { get; set; }
        public Guid? TestCaseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ActualResult { get; set; } = string.Empty;
        public TestCaseSeverity Severity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CreatedById { get; set; }

        //public List<AttachmentOutputDto> Attachments { get; set; } = new();
    }
}
