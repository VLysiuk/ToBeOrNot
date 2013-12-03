using System;
using System.Collections.Generic;
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
    public class NewIssueViewModelTests
    {
        private ISpecialTasksProvider _tasksProvider;
        private IDataProvider _dataProvider;
        private INavigationService _navigationService;

        [SetUp]
        public void SetUp()
        {
            _tasksProvider = Substitute.For<ISpecialTasksProvider>();
            _dataProvider = Substitute.For<IDataProvider>();
            _navigationService = Substitute.For<INavigationService>();
        }

        [Test]
        public void ShouldPerformPhotoChooserTask()
        {
            var newIssueViewModel = new NewIssueViewModel(_tasksProvider, _dataProvider, _navigationService);
            
            newIssueViewModel.SelectPictureCommand.Execute(null);

            _tasksProvider.Received().ChoosePicture();
        }

        [Test]
        public void ShouldPerformTakePhotoTask()
        {
            var newIssueViewModel = new NewIssueViewModel(_tasksProvider, _dataProvider, _navigationService);

            newIssueViewModel.TakePhotoCommand.Execute(null);

            _tasksProvider.Received().TakePhoto();
        }

        [Test]
        public void ShouldSaveNewIssueWithSpecifiedSubjectToDataStore()
        {
            var newIssueViewModel = new NewIssueViewModel(_tasksProvider, _dataProvider, _navigationService);
            newIssueViewModel.Subject = "Test issue";

            newIssueViewModel.SaveIssueCommand.Execute(null);

            _dataProvider.Received().Save(Arg.Is<Issue>(i => i.Subject == newIssueViewModel.Subject));
        }

        [Test]
        public void ShouldNotSaveIssueWhenSubjectIsEmpty()
        {
            var newIssueViewModel = new NewIssueViewModel(_tasksProvider, _dataProvider, _navigationService);

            newIssueViewModel.Subject = string.Empty;

            newIssueViewModel.SaveIssueCommand.Execute(null);

            _dataProvider.Received(0).Save(Arg.Any<Issue>());
        }

        [Test]
        public void ShouldNavigateToProsAndConsPageWhenIssueCreated()
        {
            var newIssueViewModel = new NewIssueViewModel(_tasksProvider, _dataProvider, _navigationService);
            newIssueViewModel.Subject = "Test issue";

            newIssueViewModel.SaveIssueCommand.Execute(null);

            _navigationService.Received()
                              .Navigate(Arg.Is<Uri>(u => u.OriginalString.Contains("ProsAndConsPage.xaml")),
                                        Arg.Any<Dictionary<string, object>>());
        }

        [Test]
        public void ShouldGoToPreviousPageWhenCancelingOperation()
        {
            var newIssueViewModel = new NewIssueViewModel(_tasksProvider, _dataProvider, _navigationService);

            newIssueViewModel.CancelIssueCommand.Execute(null);
            
            _navigationService.Received().GoBack();
        }
    }
}
