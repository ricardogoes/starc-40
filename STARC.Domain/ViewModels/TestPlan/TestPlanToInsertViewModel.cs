using System;
using System.ComponentModel.DataAnnotations;

namespace STARC.Domain.ViewModels.TestPlan
{
    public class TestPlanToInsertViewModel
    {
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public long? OwnerId { get; set; }

        [Required(ErrorMessage = "ProjectId required")]
        public long? ProjectId { get; set; }
    }
}
