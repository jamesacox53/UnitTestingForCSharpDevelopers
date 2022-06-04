using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestClass]
    public class ReservationTests
    {
        [TestMethod]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // Arrange
            Reservation reservation = new Reservation();
            User adminUser = new User() { IsAdmin = true };

            // Act 
            bool result = reservation.CanBeCancelledBy(adminUser);
            
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCancelledBy_MadeByIsUser_ReturnsTrue()
        {
            User user = new User();
            Reservation reservation = new Reservation() { MadeBy = user };
             
            bool result = reservation.CanBeCancelledBy(user);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCancelledBy_SomeoneElse_ReturnsFalse()
        {
            User user1 = new User();
            User user2 = new User();
            Reservation reservation = new Reservation() { MadeBy = user1 };

            bool result = reservation.CanBeCancelledBy(user2);

            Assert.IsFalse(result);
        }
    }
}
