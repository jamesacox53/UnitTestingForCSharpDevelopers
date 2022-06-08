using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking.InstallerHelpers
{
    internal class FileDownloader
    {
        public void DownloadFile(string url, string filePath)
        {
            WebClient client = new WebClient();
            
            client.DownloadFile(url, filePath);
        }
    }
}
