using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Data.Models.Domain
{
    public class PayrollCalculationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string?  Username { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Bonus { get; set; }
        public decimal Commission { get; set; }
        public decimal OvertimePay { get; set; }
        public decimal ExpenseToOffice { get; set; }
        public decimal GrossPay { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal PF { get; set; }
        public decimal EmployeeStateInsurance { get; set; }
        public decimal ProfessionalTax { get; set; }
        public decimal LoanRepayments { get; set; }
        public decimal LeaveDeductions { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetSalary { get; set; }
    }
}
