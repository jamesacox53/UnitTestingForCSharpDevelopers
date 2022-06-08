using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using TestNinja.Fundamentals;
using TestNinja.Mocking;
using TestNinja.Mocking.InstallerHelpers;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    internal class InstallerHelperTests
    {
        private InstallerHelper _installerHelper;
        private Mock<IFileDownloader> _fileDownloader;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            _fileDownloader.Setup(fd => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            bool result = _installerHelper.DownloadInstaller("throwAwayA", "throwAwayB");

            Assert.IsFalse(result);
        }

        [Test]
        public void DownloadInstaller_DownloadCompletes_ReturnTrue()
        {
            bool result = _installerHelper.DownloadInstaller("throwAwayA", "throwAwayB");

            Assert.IsTrue(result);
        }
    }
}
