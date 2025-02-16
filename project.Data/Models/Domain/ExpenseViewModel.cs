using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Data.Models.Domain
{
    public class ExpenseViewModel
    {
        public int UserID { get; set; }
        public string ExpenseType { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string ReceiptFileName { get; set; }

        // List of user expenses
        public List<Expense> UserExpenses { get; set; } = new List<Expense>();
        public Expense Expense { get; set; }
    }
}
