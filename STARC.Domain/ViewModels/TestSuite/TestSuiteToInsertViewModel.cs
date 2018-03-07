using System.ComponentModel.DataAnnotations;

namespace STARC.Domain.ViewModels.TestSuite
{
    public class TestSuiteToInsertViewModel
    {
        public long TestPlanId { get; set; }

        public long ParentTestSuiteId { get; set; }

        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
