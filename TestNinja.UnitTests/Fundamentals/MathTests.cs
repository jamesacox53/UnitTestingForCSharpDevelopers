using NUnit.Framework;
using System;
using TestNinja.Fundamentals;
using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class MathTests
    {
        private Math math;

        [SetUp]
        public void SetUp()
        {
            math = new Math();
        }

        [Test]
        public void Add_WhenCalled_ReturnSumOfArguments()
        {
            int result = math.Add(1, 2);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Max_FirstArgumentIsGreater_ReturnFirstArgument()
        {
            int result = math.Max(1, 2);

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Max_SecondArgumentIsGreater_ReturnSecondArgument()
        {
            int result = math.Max(3, 4);

            Assert.AreEqual(4, result);
        }

        [Test]
        public void Max_ArgumentsAreEqual_ReturnSameArgument()
        {
            int result = math.Max(5, 5);

            Assert.AreEqual(5, result);
        }
    }
}
