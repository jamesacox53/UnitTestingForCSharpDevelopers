using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator calculator;

        [SetUp]
        public void SetUp()
        {
            calculator = new DemeritPointsCalculator();
        }

        [Test]
        public void CalculateDemeritPoints_SpeedIsLessThan0_ThrowArgumentOutOfRangeException()
        {
            Assert.That(() => { calculator.CalculateDemeritPoints(-1); }, 
                        Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void CalculateDemeritPoints_SpeedIsTooMuch_ThrowArgumentOutOfRangeException()
        {
            Assert.That(() => { calculator.CalculateDemeritPoints(301); },
                        Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void CalculateDemeritPoints_SpeedIs0_Return0()
        {
            int points = calculator.CalculateDemeritPoints(0);

            Assert.That(points, Is.EqualTo(0));
        }

        [Test]
        public void CalculateDemeritPoints_SpeedIsSpeedLimit_Return0()
        {
            int points = calculator.CalculateDemeritPoints(65);

            Assert.That(points, Is.EqualTo(0));
        }

        [Test]
        public void CalculateDemeritPoints_SpeedIsLessThanLimit_Return0()
        {
            int points = calculator.CalculateDemeritPoints(1);

            Assert.That(points, Is.EqualTo(0));
        }

        [Test]
        public void CalculateDemeritPoints_SpeedIsAboveSpeedLimitButNoDemeritPoints_Return0()
        {
            int points = calculator.CalculateDemeritPoints(69);

            Assert.That(points, Is.EqualTo(0));
        }

        [Test]
        public void CalculateDemeritPoints_SpeedIs5DemeritPoints_Return5()
        {
            int points = calculator.CalculateDemeritPoints(90);

            Assert.That(points, Is.EqualTo(5));
        }
    }
}
