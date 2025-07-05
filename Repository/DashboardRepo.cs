using Microsoft.EntityFrameworkCore;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.DataContext;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.Models;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels;
using static Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums.ConstantDataSets;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.Repository
{
    public class DashboardRepo : IDashboardRepo
    {
        private readonly ApplicationDataContext _dataContext;
        public DashboardRepo(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _dataContext.Customers.Include(il => il.Tasks).ToListAsync();
        }

        public async Task<DashboardVM> GetDashboardData()
        {
            var today = DateTime.Today;
            var customers = await _dataContext.Customers
                                              .OrderBy(c => c.Id)
                                              .Select(c => new CustomerSummaryVM
                                              {
                                                  Id = c.Id,
                                                  Name = c.Name
                                              })
                                              .ToListAsync();

            var pendingTasks = await _dataContext.TaskItems
                                                 .Where(t => t.Status == TaskCurrentStatus.Pending)
                                                 .Include(t => t.Customer)
                                                 .Select(t => new TaskSummaryVM
                                                 {
                                                     Id = t.Id,
                                                     Description = t.Description,
                                                     CustomerId = t.CustomerId,
                                                     CustomerName = t.Customer.Name
                                                 })
                                                 .ToListAsync();

            var overdueTasks = await _dataContext.TaskItems
                                                 .Where(t => t.Status == TaskCurrentStatus.Pending &&
                                                             t.DueDate.HasValue &&
                                                             t.DueDate < today)
                                                 .Include(t => t.Customer)
                                                 .Select(t => new TaskSummaryVM
                                                 {
                                                     Id = t.Id,
                                                     Description = t.Description,
                                                     CustomerId = t.CustomerId,
                                                     CustomerName = t.Customer.Name
                                                 })
                                                 .ToListAsync();

            return new DashboardVM
            {
                Customers = customers,
                PendingTasks = pendingTasks,
                OverdueTasks = overdueTasks
            };
        }

        public async Task<TaskItem> GetTaskDetails(int id)
        {
            TaskItem? taskItem = await _dataContext.TaskItems
                                 .Include(t => t.Customer)
                                 .SingleOrDefaultAsync(t => t.Id == id);
            return taskItem;
        }

        public async Task SavingChangeAfterChangeStatus()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
