using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ONX.CRM.BLL.Services;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Tests
{
    public class TeacherServiceTests
    {
        private Mock<ISqlTeachersRepository<Teacher>> _teacherRepositoryMock;
        private TeacherService _teacherService;

        [SetUp]
        public void Setup()
        {
            _teacherRepositoryMock = new Mock<ISqlTeachersRepository<Teacher>>();
            _teacherService = new TeacherService(_teacherRepositoryMock.Object);
        }

        [Test]
        public void CreateMethod_Create_WasCalled()
        {
            //arrange
            var teacher = new Teacher
            {
                FirstName = "Alex",
                LastName = "Smith",
                Email = "smith@gmail.com",
                Phone = "+375291136299",
                Bio = "Nice man"
            };
            _teacherRepositoryMock.Setup(t => t.Create(teacher));
            //act
            _teacherService.Create(teacher);
            //assert
            _teacherRepositoryMock.Verify(t => t.Create(It.IsAny<Teacher>()));
        }
        [Test]
        public void GetNumberOfTeachers_ShouldReturnTaskIntValue()
        {
            //arrange
            _teacherRepositoryMock
                .Setup(x => x.GetNumberOfTeachers())
                .Returns(It.IsAny<Task<int>>);
            //act
            _teacherService.GetNumberOfTeachers();
            //assert
            _teacherRepositoryMock.Verify(t => t.GetNumberOfTeachers());
        }
        [Test]
        public void GetTeachersWithSkipAndTakeAsync_ShouldReturnIEnumerableCollection()
        {
            //arrange
            const int skip = 10;
            const int take = 10;
            _teacherRepositoryMock
                .Setup(x => x.GetTeachersWithSkipAndTakeAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(It.IsAny<Task<IEnumerable<Teacher>>>);
            //act
            _teacherService.GetTeachersWithSkipAndTakeAsync(skip, take);
            //assert
            _teacherRepositoryMock
                .Verify(s => s.GetTeachersWithSkipAndTakeAsync(skip,take));
        }
    }
}