using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.EF.Contexts;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.DAL.EF.Repositories
{
    public class SqlCoursesRepository : IRepository<Course>
    {
        private readonly Context _context;
        public SqlCoursesRepository(Context context)
        {
            _context = context;
        }
        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.AsNoTracking()
                .Include(_ => _.Groups).AsNoTracking()
                .Include(_ => _.Specialization).AsNoTracking()
                .ToList();
        }
        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.AsNoTracking()
                .Include(_ => _.Groups).AsNoTracking()
                .Include(_ => _.Specialization).AsNoTracking()
                .ToListAsync();
        }
        public Course GetEntity(int id)
        {
            return _context.Courses.Find(id);
        }
        public void Create(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }
        public void Update(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Course course = _context.Courses.Find(id);
            if (course != null)
                _context.Courses.Remove(course);
            _context.SaveChanges();
        }
    }
}
