using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace project.Data.Models.Domain
{
    public class Expense
    {
       

       
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "expense Type is required")]
        public string ExpenseType { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "ExpenseDate is required")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; }
        [Required(ErrorMessage = "Reason is required")]
        public string Description { get; set; }

        public string? ReceiptPath { get; set; } // Stores file path of uploaded receipt
    }
}
