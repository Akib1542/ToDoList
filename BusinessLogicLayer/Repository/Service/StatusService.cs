using BusinessLogicLayer.Repository.Interface;
using DatabaseAccessLayer.Data;
using DatabaseAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace BusinessLogicLayer.Repository.Service
{
    public class StatusService : IStatus
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StatusService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> AddTask(Status statuses)
        {
            context.Add(statuses);
            await context.SaveChangesAsync();
            return statuses.StatusId;
        }

        public Task<bool> DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Status>> GetMyStatus()
        {
            var data = await context.Statuses.ToListAsync();
            return data;
        }
    }
}
