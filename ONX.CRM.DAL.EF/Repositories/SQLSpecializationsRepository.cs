using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.EF.Contexts;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.DAL.EF.Repositories
{
    public class SqlSpecializationsRepository : IRepository<Specialization>
    {
        private readonly Context _context;
        public SqlSpecializationsRepository(Context context)
        {
            _context = context;
        }
        public IEnumerable<Specialization> GetAll()
        {
            return _context.Specializations.AsNoTracking().ToList();
        }
        public async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            return await _context.Specializations.AsNoTracking().ToListAsync();
        }
        public Specialization GetEntity(int id)
        {
            return _context.Specializations.Find(id);
        }
        public void Create(Specialization specialization)
        {
            _context.Specializations.Add(specialization);
            _context.SaveChanges();
        }
        public void Update(Specialization specialization)
        {
            _context.Specializations.Update(specialization);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Specialization specialization = _context.Specializations.Find(id);
            if (specialization != null)
            {
                _context.Specializations.Remove(specialization);
                _context.SaveChanges();
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
