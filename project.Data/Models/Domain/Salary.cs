using System;
using System.ComponentModel;

namespace project.Data.Models.Domain
{
    public class Salary
    {
        public int SalaryId { get; set; }
        //public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary => BasicSalary + Allowances - Deductions;
        public DateTime SalaryDate { get; set; }
        public string? Username { get; set; }
    }
}
