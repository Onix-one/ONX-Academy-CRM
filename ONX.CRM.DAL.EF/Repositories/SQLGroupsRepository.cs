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
    public class SqlGroupsRepository : ISqlGroupsRepository<Group>
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
        public async Task<IEnumerable<Group>> GetGroupsByStatus(int status)
        {
            var groups = await _context.Groups
                .Where(s => s.Status == (GroupStatus)Enum.ToObject(typeof(GroupStatus), status))
                .Include(g=>g.Course).AsNoTracking()
                .Include(g => g.Teacher).AsNoTracking().ToListAsync();
            return groups;
        }
        public async Task<IEnumerable<Group>> GetGroupsByQuery(string query)
        {
            var groups = await _context.Groups
                .Include(g => g.Students).AsNoTracking()
                .Include(g => g.Course).AsNoTracking()
                .Include(g => g.Teacher).AsNoTracking().ToListAsync();

            return groups.Where(g => g.Number.Contains(query, StringComparison.OrdinalIgnoreCase)
                                     || g.Course.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                                     || g.Teacher.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase)
                                     || g.Teacher.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)
                                     || g.StartDate.ToString().Contains(query, StringComparison.OrdinalIgnoreCase));
        }
    }
}
