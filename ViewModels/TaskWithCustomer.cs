using static Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums.ConstantDataSets;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels
{
    public class TaskWithCustomer
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Priority Priority { get; set; }
        public TaskCurrentStatus Status { get; set; }

        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
    }
}
