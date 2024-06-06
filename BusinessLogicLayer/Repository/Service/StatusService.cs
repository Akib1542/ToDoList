using DatabaseAccessLayer.Models;
using DatabaseAccessLayer.Repos;
using Microsoft.AspNetCore.Http;


namespace BusinessLogicLayer.Repository.Service
{
    public class StatusService
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly StatusRepo statusRepo;
        #endregion

        #region CTOR
        public StatusService(IHttpContextAccessor httpContextAccessor, StatusRepo statusRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            this.statusRepo = statusRepo;
        }
        #endregion

        #region AddTask
        public async Task<Status> AddTask(Status statuses)
        {
            return await statusRepo.CreateAsync(statuses);
        }
        #endregion

        #region GetMyTask
        public async Task<IEnumerable<Status>> GetMyStatus()
        {
            return await statusRepo.GellAlltaskAsync();
        }
        #endregion

        public async Task<IEnumerable<Status>> GetStatusData()
        {
            return await statusRepo.getStatusData();
        }
    }
}
