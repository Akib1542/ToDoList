using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseAccessLayer.Data;
using DatabaseAccessLayer.Models;
using BusinessLogicLayer.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BusinessLogicLayer.Repository.Service
{
    public class MyTaskService : IMyTask
    {
        #region Fields
        private readonly ApplicationDbContext context;
        #endregion

        #region CTOR
        public MyTaskService(ApplicationDbContext context)
        {
            this.context = context;
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
            if (id == null || context.Task == null || id==0)
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
        public async Task<bool> DeleteRecord(int id=0)
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
        public async Task<List<MyTask>> GetCatBySearch(string search, bool filter )
        {
            if (string.IsNullOrEmpty(search) && !filter)
            {
                var datas = context.Task.ToList();
                return datas;
            }
            
            var data = await context.Task.Where(x => x.Category == search || filter == x.IsActive).ToListAsync();
            if (search == null)
            {
                return data;
            }
            return data;
        }
        #endregion
    }
}
