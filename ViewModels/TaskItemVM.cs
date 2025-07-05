using Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums;
using static Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums.ConstantDataSets;
using System.ComponentModel.DataAnnotations;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels
{
    public class TaskItemVM
    {
        [Required(ErrorMessage ="Description required"), MaxLength(200)]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "Please select due date")]
        public DateTime? DueDate { get; set; }
        public ConstantDataSets.Priority Priority { get; set; } = Priority.Medium;
        public ConstantDataSets.TaskCurrentStatus Status { get; set; } = TaskCurrentStatus.Pending;
    }
}
