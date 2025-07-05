using static Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums.ConstantDataSets;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels
{
    public class AllTaskVM
    {
        public TaskCurrentStatus? FilterStatus { get; set; }
        public List<TaskWithCustomer> Tasks { get; set; } = new List<TaskWithCustomer>();
    }
}
