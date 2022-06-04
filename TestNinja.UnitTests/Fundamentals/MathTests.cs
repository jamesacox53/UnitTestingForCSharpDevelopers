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
        [TestCase(1, 2, 2)]
        [TestCase(3, 4, 4)]
        [TestCase(5, 5, 5)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            int result = math.Max(a, b);

            Assert.AreEqual(result, expectedResult);
        }

        [Test]
        [Ignore("Test Ignore")]
        public void Max_ArgumentsNegative_ReturnTheGreaterArgument()
        {
            int result = math.Max(-5, -4);

            Assert.AreEqual(result, -4);
        }
    }
}
