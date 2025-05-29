using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Dtos.TestCase
{
    public class UpdateTestCaseDefectsDto
    {
        public Guid TestCaseId { get; set; }
        public List<Guid> DefectIds { get; set; } = new();
    }
}
