using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Data.Models.Domain
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
        public Users User { get; set; }
    }
}
