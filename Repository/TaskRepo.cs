using Microsoft.EntityFrameworkCore;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.DataContext;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.Models;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels;
using System.Threading.Tasks;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.Repository
{
    public class TaskRepo : ITaskRepo
    {
        private readonly ApplicationDataContext _dataContext;
        public TaskRepo(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<TaskItem>> GetAllCustomerWithTask()
        {
            return await _dataContext.TaskItems
                         .Include(t => t.Customer)
                         .AsNoTracking() 
                         .OrderBy(t => t.DueDate)
                         .ToListAsync();
        }

        public async Task GetTaskByIdUpdateTaskDetails(EditSingleTaskItem vm)
        {
            TaskItem task = await _dataContext.TaskItems.FindAsync(vm.Id).AsTask();
            task.Description = vm.Description;
            task.DueDate = vm.DueDate;
            task.Priority = vm.Priority;
            task.Status = vm.Status;

            await _dataContext.SaveChangesAsync();
        }

        public async Task<TaskItem> GetTaskByIdWithCustomer(int id)
        {
            return await _dataContext.TaskItems
           .Include(t => t.Customer)
           .AsNoTracking()
           .SingleOrDefaultAsync(t => t.Id == id);
        }
    }
}
