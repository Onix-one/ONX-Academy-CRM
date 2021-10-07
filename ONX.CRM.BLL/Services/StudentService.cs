using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class StudentService : EntityService<Student>, IStudentService
    {
        public StudentService(IRepository<Student> repository) : base(repository)
        {
           
        }
    }
}
