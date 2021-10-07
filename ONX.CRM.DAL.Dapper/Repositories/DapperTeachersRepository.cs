using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.DAL.Dapper.Repositories
{
    public class DapperTeachersRepository : IRepository<Teacher>
    {
        private readonly IDbConnection _db;
        public DapperTeachersRepository(IDbConnection db)
        {
            _db = db;
        }
        public IEnumerable<Teacher> GetAll()
        {
            return _db.Query<Teacher>("SELECT * FROM Teachers").ToList();
        }
        public Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return _db.QueryAsync<Teacher>("SELECT * FROM Teachers");
        }
        public Teacher GetEntity(int id)
        {
            return _db.Query<Teacher>("SELECT * FROM Teachers WHERE Id = @id", new { id }).FirstOrDefault();
        }
        public void Create(Teacher item)
        {
            const string sqlQuery = "INSERT INTO Teachers (PNGName, WorkExperience, Bio, " +
                                    "FirstName, LastName, Email, Phone" +
                                    ") VALUES(@PNGName, @WorkExperience, @Bio, " +
                                    "@FirstName, @LastName, @Email, @Phone)";
            _db.Execute(sqlQuery, item);
        }
        public void Update(Teacher item)
        {
            const string sqlQuery = "UPDATE Teachers SET PNGName = @PNGName, WorkExperience = @WorkExperience, " +
                                    "Bio =@Bio, FirstName = @FirstName, LastName = @LastName, " +
                                    "Email = @Email, Phone = @Phone WHERE Id = @Id";
            _db.Execute(sqlQuery, item);
        }
        public void Delete(int id)
        {
            var sqlQuery = "DELETE FROM Teachers WHERE Id = @id";
            _db.Execute(sqlQuery, new { id });
        }
    }
}
