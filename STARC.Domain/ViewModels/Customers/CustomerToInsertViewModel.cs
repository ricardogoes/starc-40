using System.ComponentModel.DataAnnotations;

namespace STARC.Domain.ViewModels.Customers
{
    public class CustomerToInsertViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "DocumentId is required")]
        public string DocumentId { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}
