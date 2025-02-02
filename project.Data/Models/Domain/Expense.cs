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
       
        public int UserID { get; set; }

        [Required(ErrorMessage = "expense Type is required")]
        public string ExpenseType { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required(ErrorMessage = "ExpenseDate is required")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; }
        [Required(ErrorMessage = "Reason is required")]
        public string Description { get; set; }

        public string ReceiptFileName { get; set; }
    }
}
