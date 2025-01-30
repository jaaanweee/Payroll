using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project.Data.DataAccess;
using project.Data.Models.Domain;


namespace project.Data.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public ExpenseRepository(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            var parameters = new
            {
                EmployeeID = expense.EmployeeID,
                ExpenseType = expense.ExpenseType,
                Amount = expense.Amount,
                ExpenseDate = expense.ExpenseDate,
                Description = expense.Description,
                ReceiptPath = expense.ReceiptFileName
            };

            await _sqlDataAccess.SaveData("AddExpense", parameters);
        }
    }
}
