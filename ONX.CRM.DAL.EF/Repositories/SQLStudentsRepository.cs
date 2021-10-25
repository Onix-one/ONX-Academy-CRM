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
        public async Task<IEnumerable<Student>> GetStudentsWithSkipAndTakeAsync(int skip, int take)
        {
            return await _context.Students.AsNoTracking().Skip(skip).Take(take)
                .Include(_ => _.Group)
                .ThenInclude(_ => _.Course)
                .AsNoTracking().ToListAsync();
        }
        public async Task<int> GetNumberOfStudents()
        {
            return await _context.Students.CountAsync();
        }

        public async Task<Student> GetEntityByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
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

        private async Task<IEnumerable<Student>> GetStudentsByQuery(string query, int skip, int take)
        {
            var studentsList = await GetAllAsync();
            return studentsList
                .Where(s => s.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || s.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || s.Email.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || s.Phone.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || s.Group.Number.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Skip(skip).Take(take);
        }

        private async Task<IEnumerable<Student>> GetStudentsByCourseId(int courseId, int skip, int take)
        {
            var studentsList = await _context.Students
                .Where(s => s.Group.CourseId == courseId)
                .AsNoTracking().Skip(skip).Take(take)
                .Include(s => s.Group)
                .ThenInclude(_ => _.Course).AsNoTracking().ToListAsync();
            return studentsList;
        }

        private async Task<IEnumerable<Student>> GetStudentsByType(int type, int skip, int take)
        {
            var studentsList = await _context.Students
                .Where(s => s.Type == (StudentType)Enum.ToObject(typeof(StudentType), type))
                .AsNoTracking().Skip(skip).Take(take)
                .Include(s=>s.Group)
                .ThenInclude(_ => _.Course).AsNoTracking().ToListAsync();
            return studentsList;
        }


        private async Task<int> GetNumberOfStudentsByQuery(string query)
        {
            var studentsList = await GetAllAsync();
            return studentsList
                .Count(s => s.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || s.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || s.Email.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || s.Phone.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || s.Group.Number.Contains(query, StringComparison.OrdinalIgnoreCase));
        }
        private async Task<int> GetNumberOfStudentsByCourseId(int courseId)
        {
            return await _context.Students
                .Where(s => s.Group.CourseId == courseId)
                .AsNoTracking().CountAsync();
        }
        private async Task<int> GetNumberOfStudentsByType(int type)
        { 
            var numberOfStudentsByType = await _context.Students
                .Where(s => s.Type == (StudentType)Enum.ToObject(typeof(StudentType), type))
                .AsNoTracking().CountAsync();
            return numberOfStudentsByType;
        }
        public async Task<IEnumerable<Student>> GetListOfStudentsByParameters(string query, int courseId, int type, int skip, int take)
        {
            if (!string.IsNullOrEmpty(query))
            {
                return await GetStudentsByQuery(query, skip, take);
            }
            if (courseId != 0)
            {
                return await GetStudentsByCourseId(courseId, skip, take);
            }
            if (type != 0)
            {
                return await GetStudentsByType(type, skip, take);
            }
            return null;
        }
        public async Task<int> GetNumberOfStudentsByParameters(string query, int courseId, int type)
        {
            if (!string.IsNullOrEmpty(query))
            {
                return await GetNumberOfStudentsByQuery(query);
            }
            if (courseId != 0)
            {
                return await GetNumberOfStudentsByCourseId(courseId);
            }
            if (type != 0)
            {
              return  await GetNumberOfStudentsByType(type);
            }
            return 0;
        }
    }
}
