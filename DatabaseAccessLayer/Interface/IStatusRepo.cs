using DatabaseAccessLayer.Models;

namespace DatabaseAccessLayer.Interface
{
    public interface IStatusRepo : IRepository<Status, int>
    {
        Task<IEnumerable<Status>> GetStatusData();
    }
}