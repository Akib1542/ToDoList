using DatabaseAccessLayer.Data;
using DatabaseAccessLayer.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.Repos
{
    public class Repository<Entity, Key> : IRepository<Entity, Key> where Entity : class
    {
        #region Fields
        protected readonly ApplicationDbContext db;
        internal DbSet<Entity> dbSet;
        #endregion

        #region CTOR
        public Repository(ApplicationDbContext db)
        {
            this.db = db;
            this.dbSet = db.Set<Entity>();
        }
        #endregion

        #region Create
        public async Task<Entity> CreateAsync(Entity entity)
        {
           await dbSet.AddAsync(entity);
           await db.SaveChangesAsync();

           return entity;
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteAsync(Key id)
        {
            var data = await dbSet.FindAsync(id);
            if(data == null)
            {
                return false;
            }
            dbSet.Remove(data);
            await db.SaveChangesAsync();

            return true;
        }
        #endregion

        #region GetAllTask
        public async Task<List<Entity>> GellAlltaskAsync()
        {
           return await dbSet.ToListAsync();
        }
        #endregion

        #region Read
        public async Task<Entity> ReadAsync(Key id)
        {
            return await dbSet.FindAsync(id);
        }
        #endregion

        #region Update
        public async Task<Entity> UpdateAsync(Entity entity)
        {
            dbSet.Update(entity);
            await db.SaveChangesAsync();

            return entity;
        }
        #endregion
    }
}
