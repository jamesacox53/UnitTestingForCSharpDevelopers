using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking.VideoServices
{
    internal class VideoRepository : IVideoRepository
    {
        public IEnumerable<Video> GetUnprocessedVideos()
        {
            using (var context = new VideoContext())
            {
                List<Video> videos =
                        (from video in context.Videos
                         where !video.IsProcessed
                         select video).ToList();
                
                return videos;
            }
        }
    }
}
