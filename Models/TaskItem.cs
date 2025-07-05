using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Sanmol_Software_Assessment_AspDotNetCore_MVC.Enums.ConstantDataSets;

namespace Sanmol_Software_Assessment_AspDotNetCore_MVC.Models
{
    public class TaskItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Description { get; set; } = null!;
        public DateTime? DueDate { get; set; }
        public Priority Priority { get; set; } = Priority.Medium;
        public TaskCurrentStatus Status { get; set; } = TaskCurrentStatus.Pending;

       
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; } = null!;

    }
}
