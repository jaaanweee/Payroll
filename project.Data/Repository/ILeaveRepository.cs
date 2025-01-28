using project.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Data.Repository
{
    public interface ILeaveRepository
    {
        Task AddLeaveAsync(Leaves leave);
    }
}
