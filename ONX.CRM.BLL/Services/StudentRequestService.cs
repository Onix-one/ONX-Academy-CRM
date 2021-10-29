using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Services
{
    public class StudentRequestService : IStudentRequestService
    {
        private readonly ISqlStudentRequestsRepository<StudentRequest> _requestsRepository;
        private readonly ISqlStudentsRepository<Student> _studentsRepository;
        public StudentRequestService(ISqlStudentRequestsRepository<StudentRequest> requestsRepository, 
            ISqlStudentsRepository<Student> studentsService)
        {
            _requestsRepository = requestsRepository;
            _studentsRepository = studentsService;
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
        public void AssignRequestToGroups(IEnumerable<StudentRequest> requests, int groupId)
        {
            foreach (var request in requests)
            {
                var student = new Student()
                {
                    FirstName = request.FirstName, LastName = request.LastName,
                    Email = request.Email, Phone = request.Phone, Type = request.Type, GroupId = groupId
                };
                _studentsRepository.Create(student);
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
        public Task<IEnumerable<StudentRequest>> GetRequestsByCourseId(int courseId, int skip, int take)
        {
            return _requestsRepository.GetRequestsByCourseId(courseId, skip, take);
        }
        public Task<IEnumerable<StudentRequest>> GetRequestsWithSkipAndTakeAsync(int skip, int take)
        {
            return _requestsRepository.GetRequestsWithSkipAndTakeAsync(skip, take);
        }
        public Task<int> GetNumberOfRequests()
        {
            return _requestsRepository.GetNumberOfRequests();
        }
        public Task<int> GetNumberOfRequestsByCourseId(int courseId)
        {
            return _requestsRepository.GetNumberOfRequestsByCourseId(courseId);
        }
    }
}
