using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ToBeOrNot.Model;
using ToBeOrNot.ViewModels;

namespace ToBeOrNot.Test
{
    [TestFixture]
    public class AboutViewModelTests
    {
        [Test]
        public void ShouldRetrieveAppVersion()
        {
            const string version = "1.0.0";
            var appSettingsProvider = Substitute.For<IAppSettingsProvider>();
            appSettingsProvider.GetAppVersion().Returns(version);
            var aboutViewModel = new AboutViewModel(appSettingsProvider);

            var appVersion = aboutViewModel.Version;

            appSettingsProvider.Received().GetAppVersion();
            Assert.IsTrue(appVersion.Contains(version));
        }

        [Test]
        [Ignore]
        public void ShouldRetrieveAuthorEmailWhenSendingFeedback()
        {
            var appSettingsProvider = Substitute.For<IAppSettingsProvider>();
            var aboutViewModel = new AboutViewModel(appSettingsProvider);

            aboutViewModel.WriteAnEmailCommand.Execute(null);

            appSettingsProvider.Received().GetAuthorEmail();
        }
    }
}
