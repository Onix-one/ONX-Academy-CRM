using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class GroupService : EntityService<Group>, IGroupService
    { 
        
       
        public GroupService(IRepository<Group> repository) : base(repository)
        {
            
        }
    }
}
