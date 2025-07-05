using Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums;
using System.ComponentModel.DataAnnotations;
using static Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums.ConstantDataSets;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels
{
    public class TaskRow
    {
        public int Id { get; set; }       // 0 ⇒ new, >0 ⇒ existing
        [Required(ErrorMessage ="Description Required"), MaxLength(200)] 
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "Due Date Required")]
        public DateTime? DueDate { get; set; }
        public Priority Priority { get; set; } = Priority.Medium;
        public TaskCurrentStatus Status { get; set; } = TaskCurrentStatus.Pending;
        public bool Delete { get; set; } 
    }
}
