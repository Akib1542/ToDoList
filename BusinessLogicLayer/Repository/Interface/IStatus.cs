using DatabaseAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repository.Interface
{
    public interface IStatus
    {
        Task<IEnumerable<Status>> GetMyStatus();
        Task<string> AddTask(Status task);
        Task<bool> DeleteRecord(int id);
    }
}
