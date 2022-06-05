using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class ErrorLoggerTests
    {
        private ErrorLogger logger;
        
        [SetUp]
        public void SetUp()
        {
            logger = new ErrorLogger();
        }

        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            logger.Log("a");

            Assert.IsTrue((logger.LastError) == "a");
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string error)
        {
            Assert.That(() => { logger.Log(error); }, Throws.ArgumentNullException);
        }

        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            Guid id = Guid.Empty;
            int count = 0;

            logger.ErrorLogged += ((Object sender, Guid args) => { id = args; count++; });

            logger.Log("a");

            // Assert.That(count, Is.EqualTo(1));
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
