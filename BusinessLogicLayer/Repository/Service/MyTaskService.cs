using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseAccessLayer.Data;
using DatabaseAccessLayer.Models;
using BusinessLogicLayer.Repository.Interface;

namespace BusinessLogicLayer.Repository.Service
{
    public class MyTaskService : IMyTask
    {
        private readonly ApplicationDbContext context;

        public MyTaskService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> AddTask(MyTask task)
        {
          
               context.Add(task);
               await context.SaveChangesAsync();
               return task.Id;
        }

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

        public async Task<IEnumerable<MyTask>> GetMyTask()
        {
            var data = await context.Task.ToListAsync();
            return data;
        }

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
        private bool MyTaskExists(int id)
        {
            return (context.Task?.Any(e => e.Id == id)).GetValueOrDefault();
        }

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
    }
}
