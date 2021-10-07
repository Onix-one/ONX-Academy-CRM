using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.EF.Contexts;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.DAL.EF.Repositories
{
    public class SqlGroupsRepository : IRepository<Group>
    {
        private readonly Context _context;
        public SqlGroupsRepository(Context context)
        {
            _context = context;
        }
        public IEnumerable<Group> GetAll()
        {
            return _context.Groups
                .Include(_ => _.Students).AsNoTracking()
                .Include(_ => _.Course).AsNoTracking()
                .Include(_ => _.Teacher).AsNoTracking().ToList();
        }
        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _context.Groups
                .Include(_ => _.Students).AsNoTracking()
                .Include(_ => _.Course).AsNoTracking()
                .Include(_ => _.Teacher).AsNoTracking().ToListAsync();
        }
        public Group GetEntity(int id)
        {
            return _context.Groups.Find(id);
        }
        public void Create(Group group)
        {
            _context.Groups.Add(group);
            _context.SaveChanges();
        }
        public void Update(Group group)
        {
            _context.Groups.Update(group);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Group group = _context.Groups.Find(id);
            List<Student> students = _context.Students.AsNoTracking().ToList();
            foreach (var student in students)
            {
                if (student.GroupId == id)
                {
                    student.GroupId = null;
                    _context.Students.Update(student);
                    _context.SaveChanges();
                }
            }
            if (group != null)
            {
                _context.Groups.Remove(group);
                _context.SaveChanges();
            }
        }
    }
}
