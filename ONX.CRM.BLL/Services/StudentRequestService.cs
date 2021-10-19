using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class StudentRequestService : IStudentRequestService
    {
        private readonly ISqlStudentRequestsRepository<StudentRequest> _requestsRepository;
        private readonly ISqlStudentsRepository<Student> _studentsService;
        public StudentRequestService(ISqlStudentRequestsRepository<StudentRequest> requestsRepository, 
            ISqlStudentsRepository<Student> studentsService)
        {
            _requestsRepository = requestsRepository;
            _studentsService = studentsService;
        }
        public IEnumerable<StudentRequest> GetAll()
        {
            return _requestsRepository.GetAll();
        }
        public Task<IEnumerable<StudentRequest>> GetAllAsync()
        {
            return _requestsRepository.GetAllAsync();
        }
        public Task<StudentRequest> GetEntityByIdAsync(int id)
        {
            return _requestsRepository.GetEntityByIdAsync(id);
        }
        public void Create(StudentRequest item)
        {
            _requestsRepository.Create(item);
        }
        public void Update(StudentRequest item)
        {
            _requestsRepository.Update(item);
        }
        public void Delete(int id)
        {
            _requestsRepository.Delete(id);
        }
        public async Task<IEnumerable<StudentRequest>> GetRequestsByCourseId(int courseId)
        {
            return await _requestsRepository.GetRequestsByCourseId(courseId);
        }
        public void AssignRequestToGroups(IEnumerable<StudentRequest> requests, int groupId)
        {
            foreach (var request in requests)
            {
                var student = new Student()
                {
                    FirstName = request.FirstName, LastName = request.LastName,
                    Email = request.Email, Phone = request.Phone, Type = request.Type, GroupId = groupId
                };
                _studentsService.Create(student);
                _requestsRepository.Delete(request.Id);
            }
        }
        public async Task<Dictionary<int, string>> GetActiveCoursesIdTitle()
        {
            var courses = (await _requestsRepository.GetAllAsync()).Select(r => r.Course);
            var activeCoursesIdTitle = new Dictionary<int, string>();
            foreach (var course in courses)
            {
                activeCoursesIdTitle[course.Id] = course.Title;
            }
            return activeCoursesIdTitle;
        }
    }
}
