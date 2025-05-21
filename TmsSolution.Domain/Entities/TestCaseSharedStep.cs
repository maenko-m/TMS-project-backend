using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TmsSolution.Domain.Entities
{
    /// <summary>
    /// Связь тест-кейсов с общими шагами - Определяет, какие общие шаги используются в тест-кейсе
    /// </summary>
    public class TestCaseSharedStep
    {
        [Key] 
        public Guid Id { get; set; }
        public Guid TestCaseId { get; set; }

        [ForeignKey("TestCaseId")]
        public TestCase TestCase { get; set; }

        public Guid SharedStepId { get; set; }

        [ForeignKey("SharedStepId")]
        public SharedStep SharedStep { get; set; }

        public int Position { get; set; } // порядок шага в тест-кейсе
    }
}
