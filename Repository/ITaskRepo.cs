using Sanmol_Software_Assessment_AspDotNetCore_MVC.Models;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.Repository
{
    public interface ITaskRepo
    {
        Task<List<TaskItem>> GetAllCustomerWithTask();
        Task GetTaskByIdUpdateTaskDetails(EditSingleTaskItem vm);
        Task<TaskItem> GetTaskByIdWithCustomer(int id);
    }
}
