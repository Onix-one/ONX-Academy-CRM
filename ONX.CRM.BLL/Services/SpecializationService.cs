using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class SpecializationService : EntityService<Specialization>, ISpecializationService
    {
        public SpecializationService(IRepository<Specialization> repository) : base(repository)
        {
           
        }
    }
}
