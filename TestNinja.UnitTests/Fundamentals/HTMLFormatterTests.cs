using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class HTMLFormatterTests
    {
        private HtmlFormatter htmlFormatter;

        [SetUp]
        public void SetUp()
        {
            htmlFormatter = new HtmlFormatter();
        }
        
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
        {
            string res = htmlFormatter.FormatAsBold("abc123");

            Assert.IsTrue(res == "<strong>abc123</strong>");
        }
    }
}
