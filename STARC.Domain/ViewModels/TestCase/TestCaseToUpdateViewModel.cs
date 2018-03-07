using System.ComponentModel.DataAnnotations;

namespace STARC.Domain.ViewModels.TestCase
{
    public class TestCaseToUpdateViewModel
    {
        [Key]
        public long TestCaseId { get; set; }

        [Required(ErrorMessage = "TestSuiteId required")]
        public long? TestSuiteId { get; set; }

        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        public string Type { get; set; }

        [Required(ErrorMessage = "Description required")]
        public string Description { get; set; }

        public string PreConditions { get; set; }

        public string PosConditions { get; set; }

        public string ExpectedResult { get; set; }
    }
}