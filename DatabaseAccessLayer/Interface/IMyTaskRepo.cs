using DatabaseAccessLayer.Models;

namespace DatabaseAccessLayer.Interface
{
    public interface IMyTaskRepo : IRepository<MyTask, int>
    {
        Task<List<MyTask>> GetCatBySearch(string search, bool filter);
        Task<MyTask> GetMyDeleteTask(int? id);
        Task<MyTask> GetMyEditTask(int id);
        Task<IEnumerable<MyTask>> GetMyTaskData();
    }
}