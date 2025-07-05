using Microsoft.AspNetCore.Mvc;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.Models;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.Repository;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels;
using static Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums.ConstantDataSets;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepo _itaskRepo;
        public TaskController(ITaskRepo itaskRepo)
        {
            _itaskRepo = itaskRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Index(TaskCurrentStatus? status)
        {
            List<TaskItem> tasks = await _itaskRepo.GetAllCustomerWithTask();
            if (status != null)
                tasks = tasks.Where(t => t.Status == status).ToList();

            var vm = new AllTaskVM
            {
                FilterStatus = status,
                Tasks = tasks.Select(t => new TaskWithCustomer
                {
                    Id = t.Id,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    Status = t.Status,
                    CustomerId = t.CustomerId,
                    CustomerName = t.Customer.Name
                }).ToList()
            };

            return View(vm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _itaskRepo.GetTaskByIdWithCustomer(id);
            if (task == null) return NotFound();

            var vm = new EditSingleTaskItem
            {
                Id = task.Id,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Status = task.Status,
                CustomerName = task.Customer.Name
            };
            return View(vm);
        }

        // POST /Task/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditSingleTaskItem vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _itaskRepo.GetTaskByIdUpdateTaskDetails(vm);

            return RedirectToAction(nameof(Index));
        }
    }
}
