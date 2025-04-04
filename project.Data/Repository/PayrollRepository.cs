using Microsoft.Data.SqlClient;
using project.Data.DataAccess;
using project.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace project.Data.Repository
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public PayrollRepository(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task<int> AddPayrollAsync(PayrollCalculationModel model)
        {
            var parameters = new
            {
                model.BasicSalary,
                model.Allowance,
                model.Bonus,
                model.Commission,
                model.OvertimePay,
                model.ExpenseToOffice,
                model.GrossPay,
                model.IncomeTax,
                model.PF,
                model.EmployeeStateInsurance,
                model.ProfessionalTax,
                model.LoanRepayments,
                model.LeaveDeductions,
                model.TotalDeductions,
                model.NetSalary,
                model.Username
            };

            return await _sqlDataAccess.ExecuteAsync("usp_AddPayrollCalculation", parameters);
        }

        public async Task<IEnumerable<Users>> GetAllUsersForDropdownAsync()
        {
            return await _sqlDataAccess.GetData<Users, dynamic>("sp_GetAllUsersForDropdown", new { });
        }

        public async Task<IEnumerable<PayrollCalculationModel>> GetAllPayrollsAsync()
        {
            return await _sqlDataAccess.GetData<PayrollCalculationModel, dynamic>("GetAllPayrolls", new { });
        }

        public async Task<PayrollCalculationModel> GetPayrollByIdAsync(int id)
        {
            var result = await _sqlDataAccess.GetData<PayrollCalculationModel, dynamic>("GetPayrollById", new { Id = id });
            return result?.FirstOrDefault();
        }
    }
}
