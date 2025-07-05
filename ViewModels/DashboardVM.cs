namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels
{
    public class DashboardVM
    {
        public List<CustomerSummaryVM> Customers { get; set; } = new List<CustomerSummaryVM>();
        public List<TaskSummaryVM> PendingTasks { get; set; } = new List<TaskSummaryVM>();
        public List<TaskSummaryVM> OverdueTasks { get; set; } = new List<TaskSummaryVM>();
    }
}
