using Hotel_Manager.Core.Interfaces;
using Hotel_Manager.Core.Entities;
using Hotel_Manager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Infrastructure.Repositories
{
    public class BaseRepository<T> :IBaseRepository<T> where T : BaseEntity
    {
        protected readonly Hotel_ManagerDbContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(Hotel_ManagerDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _entities.ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _entities.FindAsync(id);

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _entities.FindAsync(id);
            if (entity != null)
            {
                _entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
