using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using TestNinja.Fundamentals;
using TestNinja.Mocking;
using TestNinja.Mocking.EmployeeControllers;
using ActionResult = TestNinja.Mocking.ActionResult;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    internal class EmployeeControllerTests
    {
        private EmployeeController _employeeController;
        private Mock<IEmployeeStorage> _employeeStorage;

        [SetUp]
        public void SetUp()
        {
            _employeeStorage = new Mock<IEmployeeStorage>();
            _employeeController = new EmployeeController(_employeeStorage.Object);
        }

        [Test]
        public void DeleteEmployeeRefactored_WhenCalled_DeleteEmployeeFromDataBase()
        {
            _employeeController.DeleteEmployeeRefactored(1);

            _employeeStorage.Verify(s => s.DeleteEmployee(1));
        }

        [Test]
        public void DeleteEmployeeRefactored_WhenCalled_ReturnsRedirectResultObject()
        {
            ActionResult result = _employeeController.DeleteEmployeeRefactored(1);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }
    }
}
