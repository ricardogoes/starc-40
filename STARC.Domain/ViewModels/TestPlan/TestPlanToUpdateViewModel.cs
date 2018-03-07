using System;
using System.ComponentModel.DataAnnotations;

namespace STARC.Domain.ViewModels.TestPlan
{
    public class TestPlanToUpdateViewModel
    {
        [Key]
        public long TestPlanId { get; set; }

        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description required")]
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public long? OwnerId { get; set; }

        [Required(ErrorMessage = "ProjectId required")]
        public long? ProjectId { get; set; }
    }
}
