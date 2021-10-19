using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.EF.Contexts;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.DAL.EF.Repositories
{
    public class SqlStudentRequestsRepository : ISqlStudentRequestsRepository<StudentRequest>
    {
        private readonly Context _context;
        public SqlStudentRequestsRepository(Context context)
        {
            _context = context;
        }
        public IEnumerable<StudentRequest> GetAll()
        {
            return _context.StudentRequests.AsNoTracking()
                .Include(_=>_.Course).AsNoTracking().ToList();
        }
        public async Task<IEnumerable<StudentRequest>> GetAllAsync()
        {
            return await _context.StudentRequests.AsNoTracking()
                .Include(_ => _.Course).AsNoTracking().ToListAsync();
        }
        public async Task<StudentRequest> GetEntityByIdAsync(int id)
        {
            return await _context.StudentRequests.FindAsync(id);
        }
        public void Create(StudentRequest request)
        {
            _context.StudentRequests.Add(request);
            _context.SaveChanges();
        }
        public void Update(StudentRequest request)
        {
            _context.StudentRequests.Update(request);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            StudentRequest request = _context.StudentRequests.Find(id);
            if (request != null)
            {
                _context.StudentRequests.Remove(request);
                _context.SaveChanges();
            }
        }
        public async Task<IEnumerable<StudentRequest>> GetRequestsByCourseId(int courseId)
        {
            var requestsList = await _context.StudentRequests
                .Where(r => r.CourseId == courseId)
                .Include(_ => _.Course).AsNoTracking().ToListAsync(); ;
            return requestsList;
        }
    }
}
