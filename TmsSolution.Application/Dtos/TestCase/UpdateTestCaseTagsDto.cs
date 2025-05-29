using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Dtos.TestCase
{
    public class UpdateTestCaseTagsDto
    {
        public Guid TestCaseId { get; set; }
        public List<Guid> TagIds { get; set; } = new();
    }
}
