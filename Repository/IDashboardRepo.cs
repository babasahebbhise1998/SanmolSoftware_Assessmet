using Sanmol_Software_Assessment_AspDotNetCore_MVC.Models;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.Repository
{
    public interface IDashboardRepo
    {
        Task<List<Customer>> GetAllCustomers();
        Task<DashboardVM> GetDashboardData();
        Task<TaskItem> GetTaskDetails(int id);
        Task SavingChangeAfterChangeStatus();
    }
}
