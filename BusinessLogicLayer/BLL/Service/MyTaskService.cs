﻿using DatabaseAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogicLayer.Repository.Interfaces;
using DatabaseAccessLayer.Interface;

namespace BusinessLogicLayer.Repository.Service
{
    public class MyTaskService : IMyTaskService
    {
        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMyTaskRepo myTaskRepo;

        #endregion

        #region CTOR

        public MyTaskService(IHttpContextAccessor httpContextAccessor, IMyTaskRepo myTaskRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            this.myTaskRepo = myTaskRepo;
        }

        #endregion

        #region AddTask

        public async Task<MyTask> AddTask(MyTask task)
        {
             task.UserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
              
             return await myTaskRepo.CreateAsync(task);
        }

        #endregion 

        #region GetDetails

        public async Task<MyTask> GetDetails(int id)
        {
            return await myTaskRepo.ReadAsync(id);
        }

        #endregion

        #region GetMyTask

        public async Task<IEnumerable<MyTask>> GetMyTask()
        {
            return await myTaskRepo.GellAlltaskAsync();
        }

        #endregion

        #region UpdateTask

        public async Task<MyTask> UpdateTask(MyTask task)
        {
            task.UserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return await myTaskRepo.UpdateAsync(task);
        }

        #endregion

        #region DeleteRecord

        public async Task<bool> DeleteRecord(int id=0)
        {
          return await myTaskRepo.DeleteAsync(id);
        }

        #endregion

        #region Search & Filter

        public async Task<List<MyTask>> GetCatBySearch(string search, bool filter )
        {
            var data = await myTaskRepo.GetCatBySearch(search, filter);
            data = SortPriority(data);

            return data;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<MyTask>> GetMyTaskDatas()
        {
            return await myTaskRepo.GetMyTaskData();
        }

        public async Task<MyTask> GetMyDeletingTask(int? id)
        {
            return await myTaskRepo.GetMyDeleteTask(id);
        }

        public async Task<MyTask>GetMyEditingTask(int id)
        {
            var Id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var task = await myTaskRepo.GetMyEditTask(id);
            if (Id != task.UserId)
            {
                return null;
            }
            return task;
        }

        #endregion

        #region SortPriority

        private static List<MyTask> SortPriority(List<MyTask> taskPriority)
        {
            taskPriority = taskPriority.OrderByDescending(n => n.Priority).ToList();

            return taskPriority;
        }

        #endregion
    }
}
