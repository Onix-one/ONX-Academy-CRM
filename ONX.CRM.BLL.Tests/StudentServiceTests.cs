using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ONX.CRM.BLL.Services;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Tests
{
    public class StudentServiceTests
    {
        private Mock<ISqlStudentsRepository<Student>> _studentRepositoryMock;
        private Mock<IRepository<Course>> _courseRepositoryMock;
        private StudentService _studentService;

        [SetUp]
        public void Setup()
        {
            _courseRepositoryMock = new Mock<IRepository<Course>>();
            _studentRepositoryMock = new Mock<ISqlStudentsRepository<Student>>();
            _studentService = new StudentService(_studentRepositoryMock.Object);
        }

        [Test]
        public async Task GetActiveCoursesIdTitle_ShouldCreateDictionary()
        {
            //arrange
            var courseCount = GetTestCourses().Count();
            var courses = _studentRepositoryMock
                .Setup(r => r.GetActiveCourses())
                .Returns(Task.Run(GetTestCourses));
            //act
            var dictionary = await _studentService.GetActiveCoursesIdTitle();
            //assert
            Assert.IsInstanceOf<Dictionary<int, string>>(dictionary);
        }
        private IEnumerable<Course> GetTestCourses()
        {
            var users = new List<Course>
            {
                new Course { Id=1, Title = "ASP.NET"},
                new Course { Id=2, Title = "Unity"},
                new Course { Id=3, Title = "Java"},
                new Course { Id=4, Title = "PHP"}
            };
            return users;
        }
    }
}