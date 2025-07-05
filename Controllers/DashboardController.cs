using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.DataContext;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.Models;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.Repository;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels;
using static Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums.ConstantDataSets;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepo _dashboardRepo;
        public DashboardController(IDashboardRepo dashboardRepo)
        {
            _dashboardRepo = dashboardRepo;
        }
        public async Task<IActionResult> Index()
        {

            DashboardVM dashboardVM = await _dashboardRepo.GetDashboardData();
            return View(dashboardVM);
        }

        public async Task<IActionResult> EditTask(int id)
        {
            TaskItem task = await _dashboardRepo.GetTaskDetails(id);

            if (task == null) return NotFound();

            var vm = new EditTaskVM
            {
                Id = task.Id,
                Description = task.Description,
                Status = task.Status
            };

            return PartialView("Partials/_EditTaskPartial", vm);
        }

        // POST /Dashboard/EditTask      (AJAX form‑post)
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTask(EditTaskVM vm)
        {
            if (!ModelState.IsValid)
                return PartialView("Partials/_EditTaskPartial", vm);

            TaskItem task = await _dashboardRepo.GetTaskDetails(vm.Id);
            if (task == null) return NotFound();

            // Only allow transition if it is still pending
            if (task.Status == TaskCurrentStatus.Pending)
                task.Status = vm.Status;

            await _dashboardRepo.SavingChangeAfterChangeStatus();
            // signal success to jQuery
            return Json(new { ok = true });
        }
        /// <summary>
        /// Export Customer And Task Data into Excel
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ExportExcel()
        {
            List<Customer> customers = await _dashboardRepo.GetAllCustomers();   // each includes Tasks

            using var wb = new XLWorkbook();

            /* ------------ Sheet 1 : Customers ------------ */
            var wsCus = wb.Worksheets.Add("Customers");
            wsCus.Cell(1, 1).Value = "Id";
            wsCus.Cell(1, 2).Value = "Name";
            wsCus.Cell(1, 3).Value = "Phone";
            wsCus.Cell(1, 4).Value = "Email";
            wsCus.Cell(1, 5).Value = "Active?";
            wsCus.Cell(1, 6).Value = "Task Count";

            var r = 2;
            foreach (var c in customers)
            {
                wsCus.Cell(r, 1).Value = c.Id;
                wsCus.Cell(r, 2).Value = c.Name;
                wsCus.Cell(r, 3).Value = c.Phone;
                wsCus.Cell(r, 4).Value = c.Email;
                wsCus.Cell(r, 5).Value = c.IsActive ? "Yes" : "No";
                wsCus.Cell(r, 6).Value = c.Tasks.Count;
                r++;
            }
            wsCus.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            wsCus.Columns().AdjustToContents();

            /* ------------ Sheet 2 : Tasks --------------- */
            var wsTask = wb.Worksheets.Add("Tasks");
            wsTask.Cell(1, 1).Value = "Task Id";
            wsTask.Cell(1, 2).Value = "Customer";
            wsTask.Cell(1, 3).Value = "Description";
            wsTask.Cell(1, 4).Value = "Due Date";
            wsTask.Cell(1, 5).Value = "Priority";
            wsTask.Cell(1, 6).Value = "Status";

            r = 2;
            foreach (var c in customers)
            {
                foreach (var t in c.Tasks)
                {
                    wsTask.Cell(r, 1).Value = t.Id;
                    wsTask.Cell(r, 2).Value = c.Name;
                    wsTask.Cell(r, 3).Value = t.Description;
                    wsTask.Cell(r, 4).Value = t.DueDate?.ToString("yyyy-MM-dd");
                    wsTask.Cell(r, 5).Value = t.Priority.ToString();
                    wsTask.Cell(r, 6).Value = t.Status.ToString();
                    r++;
                }
            }
            wsTask.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            wsTask.Columns().AdjustToContents();

            /* ------------ Return as file ---------------- */
            using var stream = new MemoryStream();
            wb.SaveAs(stream);
            stream.Position = 0;

            var fileName = $"Customers_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            const string contentType =
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(stream.ToArray(), contentType, fileName);
        }
    }
}
