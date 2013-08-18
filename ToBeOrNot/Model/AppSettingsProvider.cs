using System;
using System.Xml.Linq;

namespace ToBeOrNot.Model
{
    public class AppSettingsProvider : IAppSettingsProvider
    {
        private const string AuthorEmail = "volodymyr.lysiuk@gmail.com";

        public string GetAppVersion()
        {
            string appVersion;
            try
            {
                appVersion = XDocument.Load("WMAppManifest.xml").Root.Element("App").Attribute("Version").Value;
            }
            catch (Exception)
            {
                appVersion = string.Empty;
            }

            return appVersion;
        }

        public string GetAuthorEmail()
        {
            return AuthorEmail;
        }
    }
}
