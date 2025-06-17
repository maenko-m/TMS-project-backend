using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Enums;
using TmsSolution.Application.Dtos.TestCase;

namespace TmsSolution.Application.Dtos.TestRunTestCase
{
    public class TestRunTestCaseOutputDto
    {
        public Guid Id { get; set; }
        public Guid TestRunId { get; set; }
        public TestCaseOutputDto TestCase { get; set; }
        public TestRunTestCaseStatus Status { get; set; }
        public string? Comment { get; set; }
        public int ExecutionTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
