using Sanmol_Software_Assessment_AspDotNetCore_MVC.Models;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.Repository
{
    public interface ICustomerRepo
    {
        Task<bool> AddNewCustomer(Customer customer);
        Task<bool> DeleteCustomer(int id);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int id);
        void RemoveTaskItem(TaskItem task);
        Task SaveChangeAfterEdit();
    }
}
