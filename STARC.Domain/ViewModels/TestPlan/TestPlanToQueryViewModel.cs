using System;

namespace STARC.Domain.ViewModels.TestPlan
{
    public class TestPlanToQueryViewModel
    {
        
        public long TestPlanId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public bool Status { get; set; }

        public long? OwnerId { get; set; }

        public string Owner { get; set; }

        public long? ProjectId { get; set; }

        public string Project { get; set; }

        public long CreatedBy { get; set; }

        public string CreatedName { get; set; }

        public DateTime CreatedDate { get; set; }

        public long LastUpdatedBy { get; set; }

        public string LastUpdatedName { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
