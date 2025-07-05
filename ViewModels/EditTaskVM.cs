using System.ComponentModel.DataAnnotations;
using static Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums.ConstantDataSets;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels
{
    public class EditTaskVM
    {
        public int Id { get; set; }

        public string Description { get; set; } = "";

        [Required]
        public TaskCurrentStatus Status { get; set; }
    }
}
