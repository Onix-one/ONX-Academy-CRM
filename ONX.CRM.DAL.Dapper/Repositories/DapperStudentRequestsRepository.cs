using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.DAL.Dapper.Repositories
{
    public class DapperStudentRequestsRepository : IRepository<StudentRequest>
    {
        private readonly IDbConnection _db;
        public DapperStudentRequestsRepository(IDbConnection db)
        {
            _db = db;
        }
        public IEnumerable<StudentRequest> GetAll()
        {
            return _db.Query<StudentRequest>("SELECT * FROM StudentRequests").ToList();
        }
        public Task<IEnumerable<StudentRequest>> GetAllAsync()
        {
            return _db.QueryAsync<StudentRequest>("SELECT * FROM StudentRequests");
        }
        public StudentRequest GetEntity(int id)
        {
            return _db.Query<StudentRequest>("SELECT * FROM StudentRequests WHERE Id = @id", 
                new { id }).FirstOrDefault();
        }
        public void Create(StudentRequest item)
        {
            const string sqlQuery = "INSERT INTO StudentRequests (Created, CourseId, Type, Comments, " +
                                    "FirstName, LastName, Email, Phone" +
                                    ") VALUES(@Created, @CourseId, @Type, @Comments, " +
                                    "@FirstName, @LastName, @Email, @Phone)";
            _db.Execute(sqlQuery, item);
        }
        public void Update(StudentRequest item)
        {
            const string sqlQuery = "UPDATE StudentRequests SET Created = @Created, CourseId = @CourseId, " +
                                    "Type = @Type, Comments = @Comments" +
                                    "FirstName = @FirstName, LastName = @LastName, " +
                                    "Email = @Email, Phone = @Phone WHERE Id = @Id";
            _db.Execute(sqlQuery, item);
        }
        public void Delete(int id)
        {
            var sqlQuery = "DELETE FROM StudentRequests WHERE Id = @id";
            _db.Execute(sqlQuery, new { id });
        }
    }
}
