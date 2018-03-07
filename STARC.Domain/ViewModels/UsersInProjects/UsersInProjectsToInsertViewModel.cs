using System.ComponentModel.DataAnnotations;

namespace STARC.Domain.ViewModels.UsersInProjects
{
    public class UsersInProjectsToInsertViewModel
    {
        [Required(ErrorMessage = "UserId required")]
        public long? UserId { get; set; }

        [Required(ErrorMessage = "ProjectId required")]
        public long? ProjectId { get; set; }
    }
}
