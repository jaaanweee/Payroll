using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project.Data.Models.Domain;


namespace project.Data.Repository
{
    public interface IExpenseRepository
    {
        Task AddExpenseAsync(Expense expense);
    }
}
