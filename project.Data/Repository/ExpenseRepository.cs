using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
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
            try
            {
                var parameters = new
                {
                    UserID = expense.UserID,
                    ExpenseType = expense.ExpenseType,
                    Amount = expense.Amount,
                    ExpenseDate = expense.ExpenseDate,
                    Description = expense.Description,
                    ReceiptPath = expense.ReceiptFileName
                };

                Console.WriteLine($"Inserting Expense: {expense.ExpenseType}, Amount: {expense.Amount}");

                await _sqlDataAccess.SaveData("AddExpense", parameters);

                Console.WriteLine("Expense Inserted Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting expense: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Expense>> GetUserExpensesAsync(int userId)
        {
            var parameters = new { UserID = userId };
            return await _sqlDataAccess.GetData<Expense, dynamic>("GetUserExpenses", parameters);
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync(int id)
        {
            var parameters = new { Id = id };

            return await _sqlDataAccess.GetData<Expense, dynamic>("GetAllExpenses", parameters);
        }


        public async Task<IEnumerable<Expense>> GetExpensesByUserIdAsync(int userId)
        {
            var parameters = new { UserID = userId };
            return await _sqlDataAccess.GetData<Expense, dynamic>("GetUserExpenses", parameters);
        }


    }

}

