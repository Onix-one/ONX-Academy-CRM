using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONX.CRM.DAL.EF.Contexts;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.DAL.EF.Repositories
{
    public class SQLLessonsRepository : ISqlLessonsRepository<Lesson>
    {
        private readonly Context _context;
        public SQLLessonsRepository(Context context)
        {
            _context = context;
        }
        public IEnumerable<Lesson> GetAll()
        {
            return _context.Lessons.AsNoTracking().ToList();
        }
        public async Task<IEnumerable<Lesson>> GetAllAsync()
        {
            return await _context.Lessons.AsNoTracking().ToListAsync();
        }
        public async Task<Lesson> GetEntityByIdAsync(int id)
        {
            return await _context.Lessons.FindAsync(id);
        }
        public void Create(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            _context.SaveChanges();
        }
        public void Update(Lesson lesson)
        {
            _context.Lessons.Update(lesson);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var lesson = _context.Lessons.Find(id);
            _context.Lessons.Remove(lesson);
            _context.SaveChanges();

        }
        public async Task<IEnumerable<Lesson>> GetLessonsByGroupId(int id)
        {
            var lessons = await _context.Lessons
                .Where(s => s.GroupId == id).AsNoTracking().ToListAsync();
            return lessons;
        }

    }
}
