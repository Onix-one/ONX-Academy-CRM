using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Services
{
    public class SpecializationService : EntityService<Specialization>, ISpecializationService
    {
        public SpecializationService(IRepository<Specialization> repository) : base(repository) { }
    }
}
