using DatabaseAccessLayer.Data;
using DatabaseAccessLayer.Models;
using Microsoft.AspNetCore.Http;


namespace DatabaseAccessLayer.Repos
{
    public class StatusRepo : Repository<Status,int>
    {
        #region Fields
        private readonly ApplicationDbContext context;
        #endregion

        #region CTOR
        public StatusRepo(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.context = context;
        }
        #endregion

        #region Methods
        public async Task< IEnumerable<Status>> getStatusData()
        {
            IEnumerable<Status> data =  context.Statuses.ToList();
            return data;
        }


        /*public async Task<string> AddTask(Status statuses)
        {
            context.Add(statuses);
            await context.SaveChangesAsync();
            return statuses.StatusId;
        }

        public async Task<IEnumerable<Status>> GetMyStatus()
        {
            var data = await context.Statuses.ToListAsync();
            return data;
        }*/
        #endregion

    }
}
