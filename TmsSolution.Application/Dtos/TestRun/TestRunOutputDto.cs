using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;
using TmsSolution.Application.Dtos.Tag;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Application.Dtos.TestRunTestCase;

namespace TmsSolution.Application.Dtos.TestRun
{
    public class TestRunOutputDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Environment { get; set; }
        public Guid? MilestoneId { get; set; }
        public TestRunStatus Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<TagOutputDto> Tags { get; set; } = new();
        public List<TestRunTestCaseOutputDto> TestRunTestCases { get; set; } = new();
        public List<DefectOutputDto> Defects { get; set; } = new();
    }
}
