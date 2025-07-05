using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$",
            ErrorMessage = "Enter a valid mobile number (E.164 format).")]
        public string Phone { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
