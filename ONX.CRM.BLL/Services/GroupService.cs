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
        
       
        public GroupService(IRepository<Group> repository) : base(repository)
        {
            
        }

        public async Task<IEnumerable<Group>> SearchGroups(string query, string status)
        {
            if (!string.IsNullOrEmpty(query))
            {
                return await GetGroupsByQuery(query);
            }

            if (!string.IsNullOrEmpty(status))
            {
                return await GetGroupsByStatus(status);
            }

            return null;
        }

        private async Task<IEnumerable<Group>> GetGroupsByStatus(string status)
        {
            var groupsList = (await _repository.GetAllAsync()).Where(s => s.Status.ToString() == status);
            return groupsList;
        }

        private async Task<IEnumerable<Group>> GetGroupsByQuery(string query)
        {
            var groupsList = await _repository.GetAllAsync();
            var groups = new List<Group>();
            foreach (var group in groupsList)
            {
                if (group.Number.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    groups.Add(group);
                    continue;
                }
                if (group.Course.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    groups.Add(group);
                    continue;
                }
                if (group.Teacher.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    groups.Add(group);
                    continue;
                }
                if (group.Teacher.LastName.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    groups.Add(group);
                    continue;
                }
                if (group.StartDate.ToString().Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    groups.Add(group);
                }
            }
            return groups;
        }
    }
}
