using DatabaseAccessLayer.Models;

namespace BusinessLogicLayer.Repository.Interfaces
{
    public interface IMyTaskService
    {
        Task<MyTask> AddTask(MyTask task);
        Task<bool> DeleteRecord(int id = 0);
        Task<List<MyTask>> GetCatBySearch(string search, bool filter);
        Task<MyTask> GetDetails(int id);
        Task<MyTask> GetMyDeletingTask(int? id);
        Task<MyTask> GetMyEditingTask(int id);
        Task<IEnumerable<MyTask>> GetMyTask();
        Task<IEnumerable<MyTask>> GetMyTaskDatas();
        Task<MyTask> UpdateTask(MyTask task);
    }
}