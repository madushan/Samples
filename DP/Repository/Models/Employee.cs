using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Display(Name ="Employee Name")]
        [Required(ErrorMessage ="Name is required")]
        public string EmployeeName { get; set; }

        [Display(Name ="Address")]
        [Required(ErrorMessage ="Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Email is required")]
        public string EmailId { get; set; }
    }
}