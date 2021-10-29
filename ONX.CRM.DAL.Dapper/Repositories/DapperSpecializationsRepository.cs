using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.DAL.Dapper.Repositories
{
    public class DapperSpecializationsRepository : IRepository<Specialization>
    {
        private readonly IDbConnection _db;
        public DapperSpecializationsRepository(IDbConnection db)
        {
            _db = db;
        }
        public IEnumerable<Specialization> GetAll()
        {
            return _db.Query<Specialization>("SELECT * FROM Specializations").ToList();
        }
        public Task<IEnumerable<Specialization>> GetAllAsync()
        {
            return _db.QueryAsync<Specialization>("SELECT * FROM Specializations");
        }
        public async Task<Specialization> GetEntityByIdAsync(int id)
        {
            var specializations = await _db.QueryAsync<Specialization>("SELECT * FROM Specializations WHERE Id = @id",
                new {id});
            return specializations.FirstOrDefault();
        }
        public void Create(Specialization item)
        {
            const string sqlQuery = "INSERT INTO Specializations (Title, PNGName, " +
                                    "Description" +
                                    ") VALUES(@Type, @PNGName, " +
                                    "@Description)";
            _db.Execute(sqlQuery, item);
        }
        public void Update(Specialization item)
        {
            const string sqlQuery = "UPDATE Specializations SET Title = @Title, PNGName = @PNGName, " +
                                    "Description = @Description WHERE Id = @Id";
            _db.Execute(sqlQuery, item);
        }
        public void Delete(int id)
        {
            var sqlQuery = "DELETE FROM Specializations WHERE Id = @id";
            _db.Execute(sqlQuery, new { id });
        }
    }
}
