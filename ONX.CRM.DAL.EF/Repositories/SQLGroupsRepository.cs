using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONX.CRM.DAL.EF.Contexts;
using ONX.CRM.DAL.Enums;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

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
        public async Task<IEnumerable<Group>> GetGroupsWithSkipAndTakeAsync(int skip, int take)
        {
            return await _context.Groups.AsNoTracking().Skip(skip).Take(take)
                .Include(_ => _.Students).AsNoTracking()
                .Include(_ => _.Course).AsNoTracking()
                .Include(_ => _.Teacher).AsNoTracking().ToListAsync();
        }
        public async Task<int> GetNumberOfGroups()
        {
            return await _context.Groups.CountAsync();
        }

        public async Task<Group> GetEntityByIdAsync(int id)
        {
            return await _context.Groups.FindAsync(id);
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
        private async Task<IEnumerable<Group>> GetGroupsByQuery(string query, int skip, int take)
        {
            var groups = await GetAllAsync();
            return groups
                .Where(g => g.Number.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || g.Course.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || g.Teacher.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || g.Teacher.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || g.StartDate.ToString().Contains(query, StringComparison.OrdinalIgnoreCase))
                .Skip(skip).Take(take);
        }
        private async Task<IEnumerable<Group>> GetGroupsByStatus(int status, int skip, int take)
        {
            var groups = await _context.Groups
                .Where(s => s.Status == (GroupStatus)Enum.ToObject(typeof(GroupStatus), status))
                .AsNoTracking().Skip(skip).Take(take)
                .Include(g => g.Course).AsNoTracking()
                .Include(g => g.Teacher).AsNoTracking().ToListAsync();
            return groups;
        }
        private async Task<int> GetNumberOfGroupsByQuery(string query)
        {
            var numberOfGroupsByQuery = await GetAllAsync();
            return numberOfGroupsByQuery
                .Count(g => g.Number.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || g.Course.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || g.Teacher.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || g.Teacher.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || g.StartDate.ToString().Contains(query, StringComparison.OrdinalIgnoreCase));
        }
        private async Task<int> GetNumberOfGroupsByStatus(int status)
        {
            var numberOfGroupsByStatus = await _context.Groups
                .Where(s => s.Status == (GroupStatus)Enum.ToObject(typeof(GroupStatus), status))
                .AsNoTracking().CountAsync();
            return numberOfGroupsByStatus;
        }
        public async Task<IEnumerable<Group>> GetListOfGroupsByParameters(string query, int status, int skip, int take)
        {
            if (!string.IsNullOrEmpty(query))
            {
                return await GetGroupsByQuery(query, skip, take);
            }
            if (status != 0)
            {
                return await GetGroupsByStatus(status, skip, take);
            }
            return null;
        }
        public async Task<int> GetNumberOfGroupsByParameters(string query, int status)
        {
            if (!string.IsNullOrEmpty(query))
            {
                return await GetNumberOfGroupsByQuery(query);
            }
            if (status != 0)
            {
                return await GetNumberOfGroupsByStatus(status);
            }
            return 0;
        }
    }
}
