using project.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Data.Repository
{
    public interface IPayrollRepository
    {
        Task<int> AddPayrollAsync(PayrollCalculationModel model);
        Task<IEnumerable<Users>> GetAllUsersForDropdownAsync();
        Task<IEnumerable<PayrollCalculationModel>> GetAllPayrollsAsync(); // Added async method
        Task<PayrollCalculationModel> GetPayrollByIdAsync(int id); // Changed to async

    }
}
