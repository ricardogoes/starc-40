using System;

namespace STARC.Domain.Entities
{
    public class Project
    {
        public long ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public DateTime? StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public bool Status { get; set; }
        
        public long? OwnerId { get; set; }

        public long CustomerId { get; set; }

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
