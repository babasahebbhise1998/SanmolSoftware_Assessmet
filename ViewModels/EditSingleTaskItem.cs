using Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums;
using static Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums.ConstantDataSets;
using System.ComponentModel.DataAnnotations;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels
{
    public class EditSingleTaskItem
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Description { get; set; } = "";

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Required]
        public Priority Priority { get; set; } = Priority.Medium;

        [Required]
        public TaskCurrentStatus Status { get; set; } = TaskCurrentStatus.Pending;

        // read‑only helpers for the page
        public string CustomerName { get; set; } = "";
    }
}
