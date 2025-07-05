using Microsoft.EntityFrameworkCore;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.DataContext;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.Models;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.Repository
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly ApplicationDataContext _dataContext;
        public CustomerRepo(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> AddNewCustomer(Customer customer)
        {
            await _dataContext.AddAsync(customer);
            int rowAffected = _dataContext.SaveChanges();
            return rowAffected > 0;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await _dataContext.Customers.FindAsync(id);
            if (customer != null)
            {
                _dataContext.Customers.Remove(customer);
            }
            int rowAffected = await _dataContext.SaveChangesAsync();
            return rowAffected > 0;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            List<Customer> allCustomers = new List<Customer>();
            allCustomers =await _dataContext.Customers.Include(il => il.Tasks).ToListAsync();
            return allCustomers;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            Customer customer = new Customer();
            customer = await _dataContext.Customers.Include(il => il.Tasks).FirstOrDefaultAsync(t => t.Id == id);
            return customer;
        }

        public void RemoveTaskItem(TaskItem task)
        {
            _dataContext.TaskItems.Remove(task);
        }

        public async Task SaveChangeAfterEdit()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
