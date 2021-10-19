using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONX.CRM.BLL.Enums;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.EF.Contexts;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.DAL.EF.Repositories
{
    public class SqlStudentsRepository : ISqlStudentsRepository<Student>
    {
        private readonly Context _context;
        public SqlStudentsRepository(Context context)
        {
            _context = context;
        }
        public IEnumerable<Student> GetAll()
        {
            return _context.Students.AsNoTracking()
                .Include(_=>_.Group)
                .ThenInclude(_=>_.Course)
                .AsNoTracking().ToList();
        }
        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.AsNoTracking()
                .Include(_ => _.Group)
                .ThenInclude(_ => _.Course)
                .AsNoTracking().ToListAsync();
        }
        public Student GetEntity(int id)
        {
            return _context.Students.Find(id);
        }
        public void Create(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Student student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
        public async Task<IEnumerable<Student>> GetStudentsByType(int type)
        {
            var studentsList = await _context.Students
                .Where(s => s.Type == (StudentType)Enum.ToObject(typeof(StudentType), type))
                .Include(s=>s.Group)
                .ThenInclude(_ => _.Course).AsNoTracking().ToListAsync();
            return studentsList;
        }
        public async Task<IEnumerable<Student>> GetStudentsByCourseId(int courseId)
        {
            var studentsList = await _context.Students
                .Where(s => s.Group.CourseId == courseId)
                .Include(s => s.Group)
                .ThenInclude(_ => _.Course).AsNoTracking().ToListAsync();
            return studentsList;
        }
        public async Task<IEnumerable<Student>> GetStudentsByQuery(string query)
        {
            var allStudents = await _context.Students.AsNoTracking()
                .Include(_ => _.Group)
                .ThenInclude(_ => _.Course)
                .AsNoTracking().ToListAsync();

            return allStudents.Where(s => s.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase)
                                          || s.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)
                                          || s.Email.Contains(query, StringComparison.OrdinalIgnoreCase)
                                          || s.Phone.Contains(query, StringComparison.OrdinalIgnoreCase)
                                          || s.Group.Number.Contains(query, StringComparison.OrdinalIgnoreCase));
        }
    }
}
