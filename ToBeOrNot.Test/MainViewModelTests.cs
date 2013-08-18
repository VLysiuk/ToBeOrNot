using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ToBeOrNot.ViewModels;

namespace ToBeOrNot.Test
{
    [TestFixture]
    public class MainViewModelTests
    {
        [Test]
        public void OpenAboutCommandShouldNavigateToAboutPage()
        {
            var navigationService = Substitute.For<INavigationService>();
            var mainVieModel = new MainViewModel(navigationService);

            mainVieModel.OpenAboutCommand.Execute(null);

            navigationService.Received().Navigate(Arg.Is<Uri>(u => u.OriginalString.Contains("AboutPage.xaml")));
        }

        [Test]
        public void AddNewIssueCommandShouldNavigateToNewIssuePage()
        {
            var navigationService = Substitute.For<INavigationService>();
            var mainVieModel = new MainViewModel(navigationService);

            mainVieModel.AddNewIssueCommand.Execute(null);

            navigationService.Received().Navigate(Arg.Is<Uri>(u => u.OriginalString.Contains("NewIssuePage.xaml")));
        }
    }
}
