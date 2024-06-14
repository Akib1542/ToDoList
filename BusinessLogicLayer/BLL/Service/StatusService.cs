using BusinessLogicLayer.Repository.Interfaces;
using DatabaseAccessLayer.Interface;
using DatabaseAccessLayer.Models;
using Microsoft.AspNetCore.Http;


namespace BusinessLogicLayer.Repository.Service
{
    public class StatusService : IStatusService
    {
        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStatusRepo statusRepo;

        #endregion

        #region CTOR

        public StatusService(IHttpContextAccessor httpContextAccessor, IStatusRepo statusRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            this.statusRepo = statusRepo;
        }

        #endregion

        #region AddTask

        public async Task<Status> AddTask(Status statuses)
        {
            var searchData = await statusRepo.GetStatusData();
            var tmp = searchData.FirstOrDefault(x => x.Name.Equals(statuses.StatusId));
            if(tmp != null)
            {
                return null;
            }

            var data = await statusRepo.CreateAsync(statuses); 
            return data;
        }

        #endregion

        #region GetMyTask

        public async Task<IEnumerable<Status>> GetMyStatus()
        {
            return await statusRepo.GellAlltaskAsync();
        }

        #endregion

        #region GetStatusData

        public async Task<IEnumerable<Status>> GetStatusData()
        {
            return await statusRepo.GetStatusData();
        }

        #endregion

    }
}
