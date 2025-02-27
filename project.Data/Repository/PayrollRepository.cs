using project.Data.DataAccess;
using project.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Data.Repository
{
    public class PayrollRepository: IPayrollRepository
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
                model.NetSalary
            };

            return await _sqlDataAccess.ExecuteAsync("usp_AddPayrollCalculation", parameters);
        }


    }

}
