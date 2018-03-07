using System;

namespace STARC.Domain.Entities
{
    public class TestSuite
    {
        public long TestSuiteId { get; set; }

        public long TestPlanId { get; set; }

        public long ParentTestSuiteId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool HasChildren { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}