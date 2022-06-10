using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using TestNinja.Fundamentals;
using TestNinja.Mocking;
using TestNinja.Mocking.BookingHelpers;
using System.Linq;

namespace TestNinja.UnitTests.Mocking.BookingHelperTests
{
    [TestFixture]
    internal class OverlappingBookingsExistRefactoredTests
    {
        private Mock<IBookingRepository> _bookingRepository;
        private Booking _existing5DayBooking;
        
        [SetUp]
        public void SetUp()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _existing5DayBooking = new Booking()
            {
                Id = 2,
                ArrivalDate = ArrivalDate(2018, 1, 15),
                DepartureDate = DepartureDate(2018, 1, 20),
                Reference = "b"
            };

            _bookingRepository.Setup(br => br.GetActiveBookings(1)).Returns(new List<Booking>()
            {
                _existing5DayBooking,

            }.AsQueryable<Booking>());
        }

        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            Booking bookingThatStartsAndFinishesBefore = new Booking()
            {
                Id = 1,
                ArrivalDate = BeforeArrivalDate(_existing5DayBooking, days: 2),
                DepartureDate = BeforeArrivalDate(_existing5DayBooking, days: 1),
                Reference = "a"
            };
            
            string result = BookingHelper.OverlappingBookingsExistRefactored(
                               bookingThatStartsAndFinishesBefore, _bookingRepository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeNotInAnExistingBookingAndFinishesInAnExistingBooking_ReturnExistingBooking()
        {
            Booking bookingThatStartsBeforeButFinishesIn = new Booking()
            {
                Id = 1,
                ArrivalDate = BeforeArrivalDate(_existing5DayBooking, days: 1),
                DepartureDate = BeforeDepartureDate(_existing5DayBooking, days: 1),
                Reference = "a"
            };

            string result = BookingHelper.OverlappingBookingsExistRefactored(
                               bookingThatStartsBeforeButFinishesIn, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existing5DayBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeNotInAnExistingBookingAndFinishesAfterAnExistingBooking_ReturnExistingBooking()
        {
            Booking bookingThatStartsBeforeAndFinishesAfter = new Booking()
            {
                Id = 1,
                ArrivalDate = BeforeArrivalDate(_existing5DayBooking, days: 1),
                DepartureDate = AfterDepartureDate(_existing5DayBooking, days: 1),
                Reference = "a"
            };

            string result = BookingHelper.OverlappingBookingsExistRefactored(
                               bookingThatStartsBeforeAndFinishesAfter, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existing5DayBooking.Reference));
        }

        [Test]
        public void BookingStartsInAnExistingBookingAndFinishesInAnExistingBooking_ReturnExistingBooking()
        {
            Booking bookingThatStartsInAndFinishesIn = new Booking()
            {
                Id = 1,
                ArrivalDate = AfterArrivalDate(_existing5DayBooking, days: 1),
                DepartureDate = BeforeDepartureDate(_existing5DayBooking, days: 1),
                Reference = "a"
            };

            string result = BookingHelper.OverlappingBookingsExistRefactored(
                               bookingThatStartsInAndFinishesIn, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existing5DayBooking.Reference));
        }

        [Test]
        public void BookingStartsInAnExistingBookingAndFinishesAfterAnExistingBooking_ReturnExistingBooking()
        {
            Booking bookingThatStartsInAndFinishesIn = new Booking()
            {
                Id = 1,
                ArrivalDate = AfterArrivalDate(_existing5DayBooking, days: 1),
                DepartureDate = AfterDepartureDate(_existing5DayBooking, days: 1),
                Reference = "a"
            };

            string result = BookingHelper.OverlappingBookingsExistRefactored(
                               bookingThatStartsInAndFinishesIn, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existing5DayBooking.Reference));
        }

        [Test]
        public void BookingStartsAfterAnExistingBookingAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            Booking bookingThatStartsInAndFinishesIn = new Booking()
            {
                Id = 1,
                ArrivalDate = AfterDepartureDate(_existing5DayBooking, days: 1),
                DepartureDate = AfterDepartureDate(_existing5DayBooking, days: 2),
                Reference = "a"
            };

            string result = BookingHelper.OverlappingBookingsExistRefactored(
                               bookingThatStartsInAndFinishesIn, _bookingRepository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingIsCancelledAndOverlap_ReturnEmptyString()
        {
            Booking bookingThatStartsInAndFinishesIn = new Booking()
            {
                Id = 1,
                Status = "Cancelled",
                ArrivalDate = AfterArrivalDate(_existing5DayBooking, days: 1),
                DepartureDate = AfterDepartureDate(_existing5DayBooking, days: 1),
                Reference = "a"
            };

            string result = BookingHelper.OverlappingBookingsExistRefactored(
                               bookingThatStartsInAndFinishesIn, _bookingRepository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingIsCancelledAndNoOverlap_ReturnEmptyString()
        {
            Booking bookingThatStartsInAndFinishesIn = new Booking()
            {
                Id = 1,
                Status = "Cancelled",
                ArrivalDate = AfterDepartureDate(_existing5DayBooking, days: 1),
                DepartureDate = AfterDepartureDate(_existing5DayBooking, days: 2),
                Reference = "a"
            };

            string result = BookingHelper.OverlappingBookingsExistRefactored(
                               bookingThatStartsInAndFinishesIn, _bookingRepository.Object);

            Assert.That(result, Is.Empty);
        }

        private DateTime BeforeArrivalDate(Booking booking, int days)
        {
            return Before(booking.ArrivalDate, days);
        }

        private DateTime BeforeDepartureDate(Booking booking, int days)
        {
            return Before(booking.DepartureDate, days);
        }

        private DateTime Before(DateTime dateTime, int days)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime AfterArrivalDate(Booking booking, int days)
        {
            return After(booking.ArrivalDate, days);
        }

        private DateTime AfterDepartureDate(Booking booking, int days)
        {
            return After(booking.DepartureDate, days);
        }

        private DateTime After(DateTime dateTime, int days)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArrivalDate(int year, int month, int day)
        {
            return (new DateTime(year, month, day, 14, 0, 0));
        }

        private DateTime DepartureDate(int year, int month, int day)
        {
            return (new DateTime(year, month, day, 10, 0, 0));
        }


    }
}
