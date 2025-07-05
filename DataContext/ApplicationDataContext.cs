using Microsoft.EntityFrameworkCore;
using Sanmol_Software_Assessment_AspDotNetCore_MVC.Models;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.DataContext
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; } 
    }
}
