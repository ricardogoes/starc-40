using System.Collections.Generic;

namespace STARC.Domain.Models
{
    public class EntityValidationResult
    {
        public bool Status { get; set; }

        public List<string> ValidationMessages { get; set; }
    }
}
