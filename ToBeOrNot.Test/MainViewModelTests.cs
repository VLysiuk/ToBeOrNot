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
        public void ShouldLoadAllCurrentIssuesWhenNavigated()
        {
            _dataProvider.LoadCurrentIssues().Returns(new List<Issue> { new Issue() });
            var mainViewModel = new MainViewModel(_navigationService, _dataProvider);

            mainViewModel.NavigatedToCommand.Execute(null);

            _dataProvider.Received().LoadCurrentIssues();
            mainViewModel.CurrentIssues
                         .Should()
                         .NotBeEmpty()
                         .And.HaveCount(1);
        }

        [Test]
        public void ShouldLoadAllDecidedIssuesWhenNavigated()
        {
            _dataProvider.LoadDecidedIssues().Returns(new List<Issue> {new Issue()});
            var mainViewModel = new MainViewModel(_navigationService, _dataProvider);

            mainViewModel.NavigatedToCommand.Execute(null);

            _dataProvider.Received().LoadDecidedIssues();
            mainViewModel.DecidedIssues
                         .Should()
                         .NotBeEmpty()
                         .And.HaveCount(1);
        }

        [Test]
        public void ShouldDeleteSelectedIssue()
        {
            var mainViewModel = new MainViewModel(_navigationService, _dataProvider);
            mainViewModel.NavigatedToCommand.Execute(null);

            mainViewModel.SelectedIssue = new Issue("Test");
            mainViewModel.DeleteIssueCommand.Execute(null);

            _dataProvider.Received().DeleteIssue(mainViewModel.SelectedIssue);
        }

        [Test]
        public void ShouldNotDeleteIssueWhenItIsNotSelected()
        {
            var mainViewModel = new MainViewModel(_navigationService, _dataProvider);
            mainViewModel.SelectedIssue = null;

            var canExecute = mainViewModel.DeleteIssueCommand.CanExecute(null);

            canExecute.Should().BeFalse();
        }

        [Test]
        public void ShouldUpdateApropriateCollectionWhenDeleteIssue()
        {
            var testIssue = new Issue("Test");
            _dataProvider.LoadCurrentIssues().Returns(new List<Issue> { testIssue, new Issue("Test1") });
            _dataProvider.LoadDecidedIssues().Returns(new List<Issue>());

            var mainViewModel = new MainViewModel(_navigationService, _dataProvider);
            mainViewModel.NavigatedToCommand.Execute(null);

            mainViewModel.SelectedIssue = testIssue;
            mainViewModel.DeleteIssueCommand.Execute(null);

            mainViewModel.CurrentIssues.Should().NotContain(i => i == testIssue);
        }
    }
}
