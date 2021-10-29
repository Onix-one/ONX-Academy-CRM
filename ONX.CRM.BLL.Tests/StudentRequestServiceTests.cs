using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ONX.CRM.BLL.Services;
using ONX.CRM.DAL.Enums;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Tests
{
    public class StudentRequestServiceTests
    {
        private Mock<ISqlStudentRequestsRepository<StudentRequest>> _studentRequestRepositoryMock;
        private Mock<ISqlStudentsRepository<Student>> _studentRepositoryMock;
        private StudentRequestService _studentRequestService;

        [SetUp]
        public void Setup()
        {
            _studentRequestRepositoryMock = new Mock<ISqlStudentRequestsRepository<StudentRequest>>();
            _studentRepositoryMock = new Mock<ISqlStudentsRepository<Student>>();
            _studentRequestService = new StudentRequestService(
                _studentRequestRepositoryMock.Object, _studentRepositoryMock.Object);
        }
        [Test]
        public void AssignRequestToGroups_ShouldCreateThreeStudentsAndDeleteThreeRequests()
        {
            //arrange
            _studentRepositoryMock
                .Setup(t => t.Create(It.IsAny<Student>()));
            _studentRequestRepositoryMock
                .Setup(r => r.Delete(It.IsAny<int>()));
            //act
            _studentRequestService.AssignRequestToGroups(GetTestRequests(), 1);
            //assert
            _studentRepositoryMock
                .Verify(r=>r.Create(It.IsAny<Student>()), Times.Exactly(3));
            _studentRequestRepositoryMock
                .Verify(r => r.Delete(It.IsAny<int>()), Times.Exactly(3));
        }
        private IEnumerable<StudentRequest> GetTestRequests()
        {
            var users = new List<StudentRequest>
            {
                new StudentRequest { Id=1, FirstName = "Alex", LastName = "Smith",
                    Email = "Smith1@gmail.com", Phone = "+7900800691", Type = StudentType.Mix },
                new StudentRequest { Id=2, FirstName = "Den", LastName = "Smith",
                    Email = "Smith2@gmail.com", Phone = "+7900800692", Type = StudentType.Offline },
                new StudentRequest { Id=3, FirstName = "Ron", LastName = "Smith",
                    Email = "Smith3@gmail.com", Phone = "+7900800693", Type = StudentType.Online }
            };
            return users;
        }
    }
}