using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestNinja.Fundamentals;
using TestNinja.Mocking;
using TestNinja.Mocking.VideoServices;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    internal class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IVideoRepository> _videoRepository;

        [SetUp]
        public void SetUp()
        {
            _videoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_videoRepository.Object);
        }

        [Test]
        public void GetUnprocessedVideosAsCsvRefactored_NoUnprocessedVideos_ReturnEmptyString()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            string result = _videoService.GetUnprocessedVideosAsCsvRefactored();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsvRefactored_OneUnprocessedVideo_ReturnStringWithIdOfUnprocessedVideo()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>()
            {
                new Video() { Id = 1 },
            });

            string result = _videoService.GetUnprocessedVideosAsCsvRefactored();

            Assert.That(result, Is.EqualTo("1"));
        }


        [Test]
        public void GetUnprocessedVideosAsCsvRefactored_AFewUnprocessedVideos_ReturnStringWithIdsOfUnprocessedVideos()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>()
            {
                new Video() { Id = 1 },
                new Video() { Id = 2 },
                new Video() { Id = 3 },
            });

            string result = _videoService.GetUnprocessedVideosAsCsvRefactored();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }

    }
}
