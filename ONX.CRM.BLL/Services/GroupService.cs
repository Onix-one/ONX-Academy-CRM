using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class GroupService : EntityService<Group>, IGroupService
    {

        public GroupService(IRepository<Group> repository) : base(repository) { }

        public async Task<IEnumerable<Group>> GetGroupsByStatus(string status)
        {
            var groupsList = (await _repository.GetAllAsync()).Where(s => s.Status.ToString() == status);
            return groupsList;
        }

        public async Task<IEnumerable<Group>> GetGroupsByQuery(string query)
        {
            var allGroups = await _repository.GetAllAsync();

            return allGroups.Where(g => g.Number.Contains(query, StringComparison.OrdinalIgnoreCase)
                                             || g.Course.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                                             || g.Teacher.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase)
                                             || g.Teacher.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)
                                             || g.StartDate.ToString().Contains(query, StringComparison.OrdinalIgnoreCase));
        }
    }
}
