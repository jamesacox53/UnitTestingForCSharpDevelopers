using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private CustomerController customerController;

        [SetUp]
        public void SetUp()
        {
            customerController = new CustomerController();
        }

        [Test]
        public void GetCustomer_IdIs0_ReturnNotFound()
        {
            ActionResult result = customerController.GetCustomer(0);
            
            Assert.That(result, Is.TypeOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IdIsNot0_ReturnOk()
        {
            ActionResult result = customerController.GetCustomer(1);

            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
