using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Data.Models.Domain
{
    public class Leaves
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Leave Type is required")]
        public string LeaveType { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        public string Reason { get; set; }
    }
}
