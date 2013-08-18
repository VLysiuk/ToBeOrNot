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
            var tasksProvider = Substitute.For<ISpecialTasksProvider>();
            appSettingsProvider.GetAppVersion().Returns(version);
            var aboutViewModel = new AboutViewModel(appSettingsProvider, tasksProvider);

            var appVersion = aboutViewModel.Version;

            appSettingsProvider.Received().GetAppVersion();
            Assert.IsTrue(appVersion.Contains(version));
        }

        [Test]
        public void ShouldPerformReviewTask()
        {
            var appSettingsProvider = Substitute.For<IAppSettingsProvider>();
            var tasksProvider = Substitute.For<ISpecialTasksProvider>();
            var aboutViewModel = new AboutViewModel(appSettingsProvider, tasksProvider);

            aboutViewModel.RateAndReviewCommand.Execute(null);

            tasksProvider.Received().RateAndReview();
        }

        [Test]
        public void ShouldPerformEmailTask()
        {
            const string testEmail = "test@example.com";
            var appSettingsProvider = Substitute.For<IAppSettingsProvider>();
            appSettingsProvider.GetAuthorEmail().Returns(testEmail);
            var tasksProvider = Substitute.For<ISpecialTasksProvider>();
            var aboutViewModel = new AboutViewModel(appSettingsProvider, tasksProvider);

            aboutViewModel.WriteAnEmailCommand.Execute(null);

            tasksProvider.Received().ComposeEmail(testEmail);
        }

        [Test]
        public void ShouldRetrieveAuthorEmailWhenSendingFeedback()
        {
            var appSettingsProvider = Substitute.For<IAppSettingsProvider>();
            var tasksProvider = Substitute.For<ISpecialTasksProvider>();
            var aboutViewModel = new AboutViewModel(appSettingsProvider, tasksProvider);

            aboutViewModel.WriteAnEmailCommand.Execute(null);

            appSettingsProvider.Received().GetAuthorEmail();
        }
    }
}
