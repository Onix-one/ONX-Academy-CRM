using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONX.CRM.DAL.EF.Contexts;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.DAL.EF.Repositories
{
    public class SqlEntitiesRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly Context _context;
        private readonly DbSet<TEntity> _entities;
        public SqlEntitiesRepository(Context context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }
        public async Task<TEntity> GetEntityByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }
        public void Create(TEntity entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            _entities.Update(entity);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            TEntity entity = _entities.Find(id);
            if (entity != null)
                _entities.Remove(entity);
            _context.SaveChanges();
        }
    }
}
