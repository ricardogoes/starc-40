using System;
using System.ComponentModel.DataAnnotations;

namespace STARC.Domain.ViewModels.Projects
{
    public class ProjectToInsertViewModel
    {
        [Required(ErrorMessage ="Name required")]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public long? OwnerId { get; set; }

        [Required(ErrorMessage = "CustomerId required")]
        public long? CustomerId { get; set; }       
    }
}
