using BusinessLogicLayer.Repository.Interface;
using DatabaseAccessLayer.Data;
using DatabaseAccessLayer.Models;
using DatabaseAccessLayer.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace BusinessLogicLayer.Repository.Service
{
    public class StatusService : IStatus
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly StatusRepo statusRepo;
        #endregion

        #region CTOR
        public StatusService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, StatusRepo statusRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            this.statusRepo = statusRepo;
        }
        #endregion

        #region AddTask
        public async Task<string> AddTask(Status statuses)
        {
            return await statusRepo.AddTask(statuses);
        }
        #endregion

        #region Delete Task
        public Task<bool> DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetMyTask
        public async Task<IEnumerable<Status>> GetMyStatus()
        {
            return await statusRepo.GetMyStatus();
        }
        #endregion
    }
}
