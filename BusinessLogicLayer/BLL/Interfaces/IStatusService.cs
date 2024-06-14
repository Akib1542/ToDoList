using DatabaseAccessLayer.Models;

namespace BusinessLogicLayer.Repository.Interfaces
{
    public interface IStatusService
    {
        Task<Status> AddTask(Status statuses);
        Task<IEnumerable<Status>> GetMyStatus();
        Task<IEnumerable<Status>> GetStatusData();
    }
}