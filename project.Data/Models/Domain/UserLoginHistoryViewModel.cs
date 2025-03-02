using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Data.Models.Domain
{
    public class UserLoginHistoryViewModel
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public int? WorkingMinutes { get; set; }
    }
}
