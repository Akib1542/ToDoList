using DatabaseAccessLayer.Data;
using DatabaseAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.Repos
{
    public class MyTaskRepo
    {
        #region Fields
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region CTOR
        public MyTaskRepo(ApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        #endregion

        #region AddTask
        public async Task<int> AddTask(MyTask task)
        {
            context.Add(task);
            await context.SaveChangesAsync();
            return task.Id;
        }
        #endregion

        #region GetDetails
        public async Task<MyTask> GetDetails(int id)
        {
            if (id == null || context.Task == null || id == 0)
            {
                return null;
            }
            var myTask = await context.Task
                .Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            return myTask;
        }
        #endregion

        #region GetMyTask
        public async Task<IEnumerable<MyTask>> GetMyTask()
        {
            var data = await context.Task.ToListAsync();
            return data;
        }
        #endregion

        #region UpdateTask
        public async Task<bool> UpdateTask(MyTask task)
        {
            try
            {
                context.Update(task);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyTaskExists(task.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }
        #endregion

        #region Methods
        private bool MyTaskExists(int id)
        {
            return (context.Task?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion

        #region DeleteRecord
        public async Task<bool> DeleteRecord(int id = 0)
        {
            bool status = false;
            if (id != 0)
            {
                var data = await context.Task.FindAsync(id);
                if (data != null)
                {
                    context.Task.Remove(data);
                    await context.SaveChangesAsync();
                    status = true;
                }
            }
            return status;
        }
        #endregion

        #region Search & Filter
        public async Task<List<MyTask>> GetCatBySearch(string search, bool filter)
        {
            var datas = await context.Task.ToListAsync();
            datas = datas.Where(x => x.UserId == _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                datas = datas.Where(x => x.Category == search).ToList();
            }
            datas = datas.Where(x => x.IsActive == filter).ToList();

            return datas;
        }
        #endregion

    }
}
