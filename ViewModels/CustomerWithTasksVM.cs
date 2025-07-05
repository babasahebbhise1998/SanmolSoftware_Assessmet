using Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums;
using static Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums.ConstantDataSets;
using System.ComponentModel.DataAnnotations;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels
{
    public class CustomerWithTasksVM
    {
        public int Id { get; set; }                 // 0 ⇒ “Add”, >0 ⇒ “Edit”

        [Required] public string Name { get; set; } = null!;

        [Required, RegularExpression(@"^\+?[1-9]\d{1,14}$",
            ErrorMessage = "Enter a valid mobile number (E.164 format).")]
        public string Phone { get; set; } = null!;

        [Required, EmailAddress] public string Email { get; set; } = null!;

        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;

        // ←–– each element becomes one table‑row on the form
        public List<TaskRow> Tasks { get; set; } = new();
    }
}
