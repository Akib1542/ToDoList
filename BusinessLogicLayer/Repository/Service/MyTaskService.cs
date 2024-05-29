using DatabaseAccessLayer.Models;
using BusinessLogicLayer.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using DatabaseAccessLayer.Repos;

namespace BusinessLogicLayer.Repository.Service
{
    public class MyTaskService : IMyTask
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MyTaskRepo myTaskRepo;
        #endregion

        #region CTOR
        public MyTaskService(IHttpContextAccessor httpContextAccessor, MyTaskRepo myTaskRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            this.myTaskRepo = myTaskRepo;   
        }
        #endregion

        #region AddTask
        public async Task<int> AddTask(MyTask task)
        {
              task.UserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
              return await myTaskRepo.AddTask(task);
          
        }
        #endregion

        #region GetDetails
        public async Task<MyTask> GetDetails(int id)
        {
            return await myTaskRepo.GetDetails(id);
        }
        #endregion

        #region GetMyTask
        public async Task<IEnumerable<MyTask>> GetMyTask()
        {
            return await myTaskRepo.GetMyTask();
        }
        #endregion

        #region UpdateTask
        public async Task<bool> UpdateTask(MyTask task)
        {
            task.UserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await myTaskRepo.UpdateTask(task);
        }
        #endregion

        #region DeleteRecord
        public async Task<bool> DeleteRecord(int id=0)
        {
          return await myTaskRepo.DeleteRecord(id);
        }
        #endregion

        #region Search & Filter
        public async Task<List<MyTask>> GetCatBySearch(string search, bool filter )
        {
           return await myTaskRepo.GetCatBySearch(search, filter);  
        }
        #endregion
    }
}
