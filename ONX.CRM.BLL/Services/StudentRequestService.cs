using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class StudentRequestService : EntityService<StudentRequest>, IStudentRequestService
    {
        private readonly IStudentService _studentService;
        public StudentRequestService(IRepository<StudentRequest> repository, IStudentService studentService) : base(repository)
        {
            _studentService = studentService;
        }
        public void AssignRequestToGroups(IEnumerable<StudentRequest> requests, int groupId)
        {
            foreach (var request in requests)
            {
                //create Student
                var student = new Student()
                {
                    FirstName = request.FirstName, LastName = request.LastName,
                    Email = request.Email, Phone = request.Phone, Type = request.Type, GroupId = groupId
                };
                _studentService.Create(student);
                _repository.Delete(request.Id);
            }
        }
        public async Task<IEnumerable<StudentRequest>> GetRequestsByCourseId(int courseId)
        {
            var requestsList = (await _repository.GetAllAsync())
                .Where(r => r.CourseId == courseId);
            return requestsList;
        }
        public async Task<Dictionary<int, string>> GetCoursesForDropdown()
        {
            var courses = (await _repository.GetAllAsync()).Select(r => r.Course);
            var coursesForDropMenu = new Dictionary<int, string>();
            foreach (var course in courses)
            {
                coursesForDropMenu[course.Id] = course.Title;
            }
            return coursesForDropMenu;
        }
    }
}
