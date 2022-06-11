using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using TestNinja.Fundamentals;
using TestNinja.Mocking;
using TestNinja.Mocking.HousekeeperHelpers;
using TestNinja.Mocking.InstallerHelpers;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    internal class HousekeeperHelperTests
    {
        private HousekeeperHelper _housekeeperHelper;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _xtraMessageBox;
        private Housekeeper _housekeeper1;
        private readonly DateTime _randomTestStatementDate1 = new DateTime(2022, 5, 15);
        private string _statementFileName;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();
            _housekeeperHelper = new HousekeeperHelper(_unitOfWork.Object, _statementGenerator.Object,
                                        _emailSender.Object, _xtraMessageBox.Object);

            _statementFileName = "fileName";

            _housekeeper1 = new Housekeeper() 
            {
                Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" 
            };

            IQueryable<Housekeeper> queryReturn = (new List<Housekeeper>()
            {
                _housekeeper1
            }).AsQueryable();

            _unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(queryReturn);

            _statementGenerator.Setup(sg => sg.SaveStatement(_housekeeper1.Oid, _housekeeper1.FullName,
                                                  _randomTestStatementDate1)).Returns(() => _statementFileName);
        }
        
        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _housekeeperHelper.SendStatementEmailsRefactored(_randomTestStatementDate1);

            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper1.Oid, _housekeeper1.FullName,
                                                   _randomTestStatementDate1));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void SendStatementEmails_HouseKeepersEmailIsInput_ShouldNotGenerateStatement(string input)
        {
            _housekeeper1.Email = input;

            _housekeeperHelper.SendStatementEmailsRefactored(_randomTestStatementDate1);

            _statementGenerator.Verify(sg => sg.SaveStatement(It.IsAny<int>(), It.IsAny<string>(),
                                                   It.IsAny<DateTime>()), Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailStatement()
        {
            _housekeeperHelper.SendStatementEmailsRefactored(_randomTestStatementDate1);

            _emailSender.Verify(es => es.EmailFile(_housekeeper1.Email, _housekeeper1.StatementEmailBody,
                                            _statementFileName, It.IsAny<string>()));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void SendStatementEmails_StatementGeneratedFileNameOutputIsInput_ShouldNotGenerateStatement(string input)
        {
            _statementFileName = input;

            _housekeeperHelper.SendStatementEmailsRefactored(_randomTestStatementDate1);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(),
                                           It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();

            _housekeeperHelper.SendStatementEmailsRefactored(_randomTestStatementDate1);

            _xtraMessageBox.Verify(xmb => xmb.Show(It.IsAny<string>(), It.IsAny<string>(),
                                                 MessageBoxButtons.OK));
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(),
                                            It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }
    }
}
