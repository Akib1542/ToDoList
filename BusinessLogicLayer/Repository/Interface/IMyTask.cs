using DatabaseAccessLayer.Models;

namespace BusinessLogicLayer.Repository.Interface
{
    public interface IMyTask
    {
        Task<IEnumerable<MyTask>> GetMyTask();
        Task<int> AddTask(MyTask task);
        Task<MyTask> GetDetails(int Id);
        Task<bool> DeleteRecord(int id);
        Task<bool> UpdateTask(MyTask task);
        Task<List<MyTask>> GetCatBySearch(string search, bool filter);
    }
}
