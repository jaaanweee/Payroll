using project.Data.DataAccess;
using project.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Data.Repository
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public LeaveRepository(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public async Task AddLeaveAsync(Leaves leave)
        {
            var parameters = new
            {
                UserID = leave.UserID,
                LeaveType = leave.LeaveType,
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                Reason = leave.Reason
            };

            // Assuming you have a stored procedure named AddUser
            await _sqlDataAccess.SaveData("AddLeave", parameters);
        }

    }

}