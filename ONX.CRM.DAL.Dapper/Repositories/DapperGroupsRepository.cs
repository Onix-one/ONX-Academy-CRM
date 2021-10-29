using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.DAL.Dapper.Repositories
{
    public class DapperGroupsRepository : IRepository<Group>
    {
        private readonly IDbConnection _db;
        public DapperGroupsRepository(IDbConnection db)
        {
            _db = db;
        }
        public IEnumerable<Group> GetAll()
        {
            return _db.Query<Group>("SELECT * FROM Groups").ToList();
        }
        public Task<IEnumerable<Group>> GetAllAsync()
        {
            return _db.QueryAsync<Group>("SELECT * FROM Groups");
        }
        public async Task<Group> GetEntityByIdAsync(int id)
        {
            var groups = await _db.QueryAsync<Group>("SELECT * FROM Groups WHERE Id = @id", new {id});
            return groups.FirstOrDefault();
        }
        public void Create(Group item)
        {
            const string sqlQuery = "INSERT INTO Groups (Number, CourseId, " +
                                    "TeacherId, StartDate, Status" +
                                    ") VALUES(@Number, @CourseId, " +
                                    "@TeacherId, @StartDate, @Status)";
            _db.Execute(sqlQuery, item);
        }
        public void Update(Group item)
        {
            const string sqlQuery = "UPDATE Groups SET Number = @Number, CourseId = @CourseId, " +
                                    "TeacherId = @TeacherId, StartDate = @StartDate, " +
                                    "Status = @Status WHERE Id = @Id";
            _db.Execute(sqlQuery, item);
        }
        public void Delete(int id)
        {
            var sqlQuery = "DELETE FROM Groups WHERE Id = @id";
            _db.Execute(sqlQuery, new { id });
        }

    }
}
