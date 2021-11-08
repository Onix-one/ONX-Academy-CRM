using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONX.CRM.DAL.EF.Contexts;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.DAL.EF.Repositories
{
    public class SqlManagersRepository : ISqlManagersRepository<Manager>
    {
        private readonly Context _context;
        public SqlManagersRepository(Context context)
        {
            _context = context;
        }
        public IEnumerable<Manager> GetAll()
        {
            return _context.Managers.AsNoTracking().ToList();
        }
        public async Task<IEnumerable<Manager>> GetAllAsync()
        {
            return await _context.Managers.AsNoTracking().ToListAsync();
        }
        public async Task<Manager> GetEntityByIdAsync(int id)
        {
            return await _context.Managers.FindAsync(id);
        }
        public void Create(Manager manager)
        {
            _context.Managers.Add(manager);
            _context.SaveChanges();
        }
        public void Update(Manager manager)
        {
            _context.Managers.Update(manager);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Manager manager = _context.Managers.Find(id);
            if (manager != null)
                _context.Managers.Remove(manager);
            _context.SaveChanges();
        }
        public async Task<bool> CheckIfManagerExists(string email)
        {
            if (await _context.Managers.Where(m=>m.Email.Contains(email)).CountAsync() != 0)
            {
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<Manager>> FindByUserIdAsync(string userId)
        {
            return await _context.Managers.Where(m => m.UserId.Contains(userId)).AsNoTracking().ToListAsync();
        }
    }
}
