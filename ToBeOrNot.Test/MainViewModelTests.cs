using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ToBeOrNot.Model;
using ToBeOrNot.Model.Data;
using ToBeOrNot.ViewModels;
using ToBeOrNot.ViewModels.Navigation;

namespace ToBeOrNot.Test
{
    [TestFixture]
    public class MainViewModelTests
    {
        private IDataProvider _dataProvider;
        private INavigationService _navigationService;

        [SetUp]
        public void SetUp()
        {
            _dataProvider = Substitute.For<IDataProvider>();
            _navigationService = Substitute.For<INavigationService>();
        }

        [Test]
        public void OpenAboutCommandShouldNavigateToAboutPage()
        {
            var mainVieModel = new MainViewModel(_navigationService, _dataProvider);

            mainVieModel.OpenAboutCommand.Execute(null);

            _navigationService.Received().Navigate(Arg.Is<Uri>(u => u.OriginalString.Contains("AboutPage.xaml")));
        }

        [Test]
        public void AddNewIssueCommandShouldNavigateToNewIssuePage()
        {
            var mainVieModel = new MainViewModel(_navigationService, _dataProvider);

            mainVieModel.AddNewIssueCommand.Execute(null);

            _navigationService.Received().Navigate(Arg.Is<Uri>(u => u.OriginalString.Contains("NewIssuePage.xaml")));
        }

        [Test]
        public void ShouldLoadAllCurrentIssuesWhenCreated()
        {
            _dataProvider.LoadCurrentIssues().Returns(new List<Issue> { new Issue() });
            var mainViewModel = new MainViewModel(_navigationService, _dataProvider);

            _dataProvider.Received().LoadCurrentIssues();
            mainViewModel.CurrentIssues
                         .Should()
                         .NotBeEmpty()
                         .And.HaveCount(1);
        }

        [Test]
        public void ShouldLoadAllDecidedIssuesWhenCreated()
        {
            _dataProvider.LoadDecidedIssues().Returns(new List<Issue> {new Issue()});
            var mainViewModel = new MainViewModel(_navigationService, _dataProvider);

            _dataProvider.Received().LoadDecidedIssues();
            mainViewModel.DecidedIssues
                         .Should()
                         .NotBeEmpty()
                         .And.HaveCount(1);
        }
    }
}
