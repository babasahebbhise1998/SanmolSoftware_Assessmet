using Microsoft.AspNetCore.Mvc;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.Models;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.Repository;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.ViewModels;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepo _customerRepo;
        public CustomerController(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? name, string? phone)
        {
            // List<Customer>
            var customers = await _customerRepo.GetAllCustomers();   

            // -----------------------  Filtering  -----------------------
            if (!string.IsNullOrWhiteSpace(name))
                customers = customers
                            .Where(c => c.Name.Contains(name,
                                       StringComparison.OrdinalIgnoreCase))
                            .ToList();

            if (!string.IsNullOrWhiteSpace(phone))
                customers = customers
                            .Where(c => c.Phone.Contains(phone))
                            .ToList();
            var list = customers.Select(c => new CustomerWithTasksVM
            {
                Id = c.Id,
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email,
                IsActive = c.IsActive,
                Tasks = c.Tasks.Select(t => new TaskRow
                {
                    Id = t.Id,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    Status = t.Status
                }).ToList()
            }).ToList();

            ViewData["NameSearch"] = name;
            ViewData["PhoneSearch"] = phone;

            return View(list);
        }

        // POST: /Customer/Delete/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDelete = await _customerRepo.DeleteCustomer(id);
            return RedirectToAction(nameof(Index));
        }


        // GET: /Customer/Create
        [HttpGet]
        public IActionResult Create() 
        {
            return View(new CustomerWithTasksVM());
        }

        // POST: /Customer/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerWithTasksVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var customer = new Customer
            {
                Name = vm.Name,
                Phone = vm.Phone,
                Email = vm.Email,
                Address = vm.Address,
                IsActive = vm.IsActive,
                Tasks = vm.Tasks
                           .Where(t => !t.Delete)          // ignore rows the user ticked “delete”
                           .Select(t => new TaskItem
                           {
                               Description = t.Description,
                               DueDate = t.DueDate,
                               Priority = t.Priority,
                               Status = t.Status
                           })
                           .ToList()
            };

            bool isadded = await _customerRepo.AddNewCustomer(customer);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Customer/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Customer customer = await _customerRepo.GetCustomerById(id);
            if (customer == null) return NotFound();

            var vm = new CustomerWithTasksVM
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                IsActive = customer.IsActive,
                Tasks = customer.Tasks.Select(t => new TaskRow
                {
                    Id = t.Id,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    Status = t.Status,
                    Delete = false
                }).ToList()
            };

            return View(vm);
        }

        // POST: /Customer/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerWithTasksVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            Customer customer = await _customerRepo.GetCustomerById(vm.Id);

            // update scalar fields
            customer.Name = vm.Name;
            customer.Phone = vm.Phone;
            customer.Email = vm.Email;
            customer.Address = vm.Address;
            customer.IsActive = vm.IsActive;

            // sync tasks
            foreach (var row in vm.Tasks)
            {
                if (row.Id == 0 && !row.Delete)
                {
                    // new task
                    customer.Tasks.Add(new TaskItem
                    {
                        Description = row.Description,
                        DueDate = row.DueDate,
                        Priority = row.Priority,
                        Status = row.Status
                    });
                    continue;
                }
                else
                {
                    var task = customer.Tasks.Single(t => t.Id == row.Id);
                    if (task == null)
                        continue;
                    if (row.Delete)
                        _customerRepo.RemoveTaskItem(task);
                    else
                    {
                        task.Description = row.Description;
                        task.DueDate = row.DueDate;
                        task.Priority = row.Priority;
                        task.Status = row.Status;
                    }
                }
            }

            await _customerRepo.SaveChangeAfterEdit();
            return RedirectToAction(nameof(Index));
        }
    }

}
