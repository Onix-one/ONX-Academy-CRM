using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.EF.Contexts;
using ONX.CRM.DAL.Interfaces;


namespace ONX.CRM.DAL.EF.Repositories
{
    public class SqlTeachersRepository : ISqlTeachersRepository<Teacher>
    {
        private readonly Context _context;
        public SqlTeachersRepository(Context context)
        {
            _context = context;
        }
        public IEnumerable<Teacher> GetAll()
        {
            return _context.Teachers.AsNoTracking().ToList();
        }
        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Teachers.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Teacher>> GetTeachersWithSkipAndTakeAsync(int skip, int take)
        {
            return await _context.Teachers.AsNoTracking().Skip(skip).Take(take).ToListAsync();
        }
        public async Task<int> GetNumberOfTeachers()
        {
            return await _context.Teachers.CountAsync();
        }
        public async Task<Teacher> GetEntityByIdAsync(int id)
        {
            return await _context.Teachers.FindAsync(id);
        }
        public void Create(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
        }
        public void Update(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Teacher teacher = _context.Teachers.Find(id);
            List<Group> groups = _context.Groups.AsNoTracking().ToList();
            foreach (var group in groups)
            {
                if (group.TeacherId == id)
                {
                    group.TeacherId = null;
                    _context.Groups.Update(group);
                    _context.SaveChanges();
                }
            }
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                _context.SaveChanges();
            }
        }
        public async Task<IEnumerable<Teacher>> GetTeachersByQuery(string query, int skip, int take)
        {
            var teachers = await GetAllAsync();
            return teachers
                .Where(s => s.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || s.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || s.Email.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || s.Phone.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || s.WorkExperience.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Skip(skip).Take(take);
        }
        public async Task<int> GetNumberOfTeachersByQuery(string query)
        {
            var numberOfTeachers = await GetAllAsync();
            return numberOfTeachers.Count(s => s.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase)
                                               || s.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)
                                               || s.Email.Contains(query, StringComparison.OrdinalIgnoreCase)
                                               || s.Phone.Contains(query, StringComparison.OrdinalIgnoreCase)
                                               || s.WorkExperience.Contains(query, StringComparison.OrdinalIgnoreCase));
        }
    }
}
