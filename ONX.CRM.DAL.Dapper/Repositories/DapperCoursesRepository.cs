using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.DAL.Dapper.Repositories
{
    public class DapperCoursesRepository : IRepository<Course>
    {
        private readonly IDbConnection _db;
        public DapperCoursesRepository(IDbConnection db)
        {
            _db = db;
        }
        public IEnumerable<Course> GetAll()
        {
            return _db.Query<Course>("SELECT * FROM Courses").ToList();
        }
        public Task<IEnumerable<Course>> GetAllAsync()
        {
            return _db.QueryAsync<Course>("SELECT * FROM Courses");
        }
        public async Task<Course> GetEntityByIdAsync(int id)
        {
            var courses = await _db.QueryAsync<Course>("SELECT * FROM Courses WHERE Id = @id", new {id});
            return courses.FirstOrDefault();
        }
        public void Create(Course item)
        {
            const string sqlQuery = "INSERT INTO Courses (Title, Description, " +
                                    "NecessaryPreKnowledge, Cost, SpecializationId" +
                                    ") VALUES(@Title, @Description, " +
                                    "@NecessaryPreKnowledge, @Cost, @SpecializationId)";
            _db.Execute(sqlQuery, item);
        }
        public void Update(Course item)
        {
            const string sqlQuery = "UPDATE Courses SET Title = @Title, Description = @Description, " +
                                    "NecessaryPreKnowledge = @NecessaryPreKnowledge, Cost = @Cost, " +
                                    "SpecializationId = @SpecializationId WHERE Id = @Id";
            _db.Execute(sqlQuery, item);
        }
        public void Delete(int id)
        {
            var sqlQuery = "DELETE FROM Courses WHERE Id = @id";
            _db.Execute(sqlQuery, new { id });
        }
    }
}
