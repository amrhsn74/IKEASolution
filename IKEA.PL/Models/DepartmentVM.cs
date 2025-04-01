using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.Models
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name = " Date of Creation")]
        public DateOnly CreationDate { get; set; }
    }
}
