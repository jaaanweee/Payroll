using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Data.Models.Domain
{
    public class SalarySlip
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Deduction { get; set; }
        public decimal NetSalary { get; set; }
        public string AmountInWords { get; set; }
    }
}
