using System.Net;
using TestNinja.Mocking.InstallerHelpers;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private string _setupDestinationFile;
        private IFileDownloader _fileDownloader;

        public InstallerHelper(IFileDownloader fileDownloader = null)
        {
            _fileDownloader = fileDownloader;
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            string webAddress = $"http://example.com/{customerName}/{installerName}";

            try
            {
                _fileDownloader.DownloadFile(webAddress, _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false; 
            }
        }
    }
}