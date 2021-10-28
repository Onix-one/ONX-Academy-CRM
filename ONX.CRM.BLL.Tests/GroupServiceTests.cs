using Moq;
using NUnit.Framework;
using ONX.CRM.BLL.Services;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Tests
{
    public class GroupServiceTests
    {
        private Mock<ISqlGroupsRepository<Group>> _groupRepositoryMock;
        private GroupService _groupService;

        [SetUp]
        public void Setup()
        {
            _groupRepositoryMock = new Mock<ISqlGroupsRepository<Group>>();
            _groupService = new GroupService(_groupRepositoryMock.Object);
        }
        [Test]
        public void GetListOfGroupsByParametersMethod_GetListOfGroupsByParameters_WasCalled()
        {
            //arrange
            _groupRepositoryMock.Setup(t => t.GetListOfGroupsByParameters(
                It.IsAny<string>(),It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));
            //act
            _groupService.GetListOfGroupsByParameters(
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());
            //assert
            _groupRepositoryMock.VerifyAll();
        }
    }
}