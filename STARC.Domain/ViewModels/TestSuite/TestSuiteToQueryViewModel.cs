using System;

namespace STARC.Domain.ViewModels.TestSuite
{
    public class TestSuiteToQueryViewModel
    {
        public long TestSuiteId { get; set; }

        public long TestPlanId { get; set; }

        public string TestPlan { get; set; }

        public long ParentTestSuiteId { get; set; }

        public string ParentTestSuite { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool HasChildren { get; set; }

        public long CreatedBy { get; set; }

        public string CreatedName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
