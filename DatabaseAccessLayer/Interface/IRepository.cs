using DatabaseAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.Interface
{
    public interface IRepository<Entity, Key>
    {
        Task<Entity> CreateAsync(Entity  entity);
        Task<Entity> UpdateAsync(Entity entity);
        Task<bool> DeleteAsync(Key id);
        Task<List<Entity>> GellAlltaskAsync();
        Task<Entity> ReadAsync(Key id);
    }
}
