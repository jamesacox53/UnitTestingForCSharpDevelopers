using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class FizzBuzzTests
    { 
        [Test]
        [TestCase(15, "FizzBuzz")]
        [TestCase(-30, "FizzBuzz")]
        [TestCase(9, "Fizz")]
        [TestCase(-6, "Fizz")]
        [TestCase(20, "Buzz")]
        [TestCase(-40, "Buzz")]
        [TestCase(26, "26")]
        [TestCase(-11, "-11")]
        public void GetOutput_NumberEntered_ReturnsCorrectString(int input, string expectedResult)
        {
            string result = FizzBuzz.GetOutput(input);

            Assert.That(expectedResult, Is.EqualTo(result));
        }


    }
}
