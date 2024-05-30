﻿using DatabaseAccessLayer.Data;
using DatabaseAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace DatabaseAccessLayer.Repos
{
    public class MyTaskRepo : Repository<MyTask, int>
    {
        #region Fields
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region CTOR
        public MyTaskRepo(ApplicationDbContext context,IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.context = context;
            _httpContextAccessor = httpContextAccessor;
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