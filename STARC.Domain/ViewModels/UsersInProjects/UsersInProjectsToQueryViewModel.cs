using System;

namespace STARC.Domain.ViewModels.UsersInProjects
{
    public class UsersInProjectsToQueryViewModel
    {
        public long UserInProjectId { get; set; }

        public long UserId { get; set; }

        public string User { get; set; }

        public long ProjectId { get; set; }

        public string Project { get; set; }

        public long CreatedBy { get; set; }

        public string CreatedName { get; set; }

        public DateTime CreatedDate { get; set; }        
    }
}
