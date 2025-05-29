using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Application.Dtos.TestRunTestCase
{
    public class TestRunTestCaseUpdateDto
    {
        public Guid? TestRunId { get; set; }
        public Guid? TestCaseId { get; set; }
        public TestRunTestCaseStatus? Status { get; set; }

        [StringLength(255, ErrorMessage = "Test run test case cannot exceed 255 characters.")]
        public string? Comment { get; set; }
        public int? ExecutionTime { get; set; }
    }
}
