using BudgetCalculator.Data.Base;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BudgetCalculator.Models
{
    public class CostCenterEntityVM 
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Cost Center")]
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; } = null;


       

        [DisplayName("Department ")]
        
        public int DepartmentId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? LastUpdatedAt { get; set; }
        public string? LastUpdatedBy { get; set; }

    }
}
