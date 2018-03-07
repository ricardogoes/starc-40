using System;

namespace STARC.Domain.Entities
{
    public class TestCase
    {
        public long TestCaseId { get; set; }

        public long TestSuiteId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public string PreConditions { get; set; }

        public string PosConditions { get; set; }

        public string ExpectedResult { get; set; }

        public bool Status { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long LastUpdatedBy { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public void ChangeStatus()
        {
            this.Status = this.Status ? false : true;
        }
    }
}