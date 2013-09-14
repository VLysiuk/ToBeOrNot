using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using ToBeOrNot.Model;
using ToBeOrNot.Model.Data;
using ToBeOrNot.ViewModels;
using ToBeOrNot.ViewModels.Navigation;

namespace ToBeOrNot.Test
{
    [TestFixture]
    public class ProsAndConsViewModelTests
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
        public void ShouldExtractCurrentIssueKeyWhenCreated()
        {
            var testIssue = new Issue {Subject = "Test"};
            _dataProvider.LoadIssue(Arg.Any<int>()).Returns(testIssue);

            var prosAndConsViewModel = new ProsAndConsViewModel(_dataProvider, _navigationService);
            _navigationService.Received().GetNavigationParameter<int>(Arg.Any<string>());
        }

        [Test]
        public void ShouldExtractCurrentIssueWhenCreated()
        {
            var testIssue = new Issue { Subject = "Test" };
            _dataProvider.LoadIssue(Arg.Any<int>()).Returns(testIssue);

            var prosAndConsViewModel = new ProsAndConsViewModel(_dataProvider, _navigationService);
            _dataProvider.Received().LoadIssue(Arg.Any<int>());
        }

        [Test]
        public void ShouldSetSubjectWhenCreated()
        {
            var testIssue = new Issue { Subject = "Test" };
            _dataProvider.LoadIssue(Arg.Any<int>()).Returns(testIssue);
            var prosAndConsViewModel = new ProsAndConsViewModel(_dataProvider, _navigationService);

            Assert.AreEqual("Test", prosAndConsViewModel.Subject);
        }

        [Test]
        public void ShouldSetProsAndConsListsWhenCreated()
        {
            var testIssue = new Issue { Subject = "Test" };
            testIssue.AddPositivePoint(new EvaluationItem());
            testIssue.AddNegativePoint(new EvaluationItem());

            _dataProvider.LoadIssue(Arg.Any<int>()).Returns(testIssue);
            var prosAndConsViewModel = new ProsAndConsViewModel(_dataProvider, _navigationService);

            Assert.IsTrue(prosAndConsViewModel.ProsItems.Count == 1);
            Assert.IsTrue(prosAndConsViewModel.ConsItems.Count == 1);
        }

        [Test]
        public void ShouldApplyAllChangesToCurrentIssueWhenDecideLater()
        {
            var testIssue = new Issue { Subject = "Test" };
            _dataProvider.LoadIssue(Arg.Any<int>()).Returns(testIssue);
            var prosAndConsViewModel = new ProsAndConsViewModel(_dataProvider, _navigationService);

            const string newSubject = "new subject";
            prosAndConsViewModel.Subject = newSubject;
            prosAndConsViewModel.ConsItems.Add(new EvaluationItem());
            prosAndConsViewModel.ProsItems.Add(new EvaluationItem());

            prosAndConsViewModel.DecideLaterCommand.Execute(null);
            Assert.AreEqual(newSubject, testIssue.Subject);
            Assert.AreEqual(1, testIssue.PositivePoints.Count());
            Assert.AreEqual(1, testIssue.NegativePoints.Count());
        }

        [Test]
        public void ShouldSaveIssueWhenDecideLater()
        {
            var testIssue = new Issue { Subject = "Test" };
            _dataProvider.LoadIssue(Arg.Any<int>()).Returns(testIssue);
            var prosAndConsViewModel = new ProsAndConsViewModel(_dataProvider, _navigationService);

            prosAndConsViewModel.DecideLaterCommand.Execute(null);

            _dataProvider.Received().Save(testIssue);
        }

        [Test]
        public void ShouldNavigateToMainPageWhenDecideLater()
        {
            var testIssue = new Issue { Subject = "Test" };
            _dataProvider.LoadIssue(Arg.Any<int>()).Returns(testIssue);
            var prosAndConsViewModel = new ProsAndConsViewModel(_dataProvider, _navigationService);

            prosAndConsViewModel.DecideLaterCommand.Execute(null);

            _navigationService.Received().Navigate(Arg.Is<Uri>(uri => uri.OriginalString.Contains("MainPage.xaml")));
        }

        [Test]
        public void ShouldNotSetDecisionWhenDecideLater()
        {
            var testIssue = new Issue { Subject = "Test" };
            _dataProvider.LoadIssue(Arg.Any<int>()).Returns(testIssue);
            var prosAndConsViewModel = new ProsAndConsViewModel(_dataProvider, _navigationService);

            prosAndConsViewModel.DecideLaterCommand.Execute(null);

            Assert.AreEqual(Decision.None, testIssue.Decision);
        }

        [Test]
        public void ShouldOpenPopupWhenOpenDecidePromptCommand()
        {
            var testIssue = new Issue { Subject = "Test" };
            _dataProvider.LoadIssue(Arg.Any<int>()).Returns(testIssue);
            var prosAndConsViewModel = new ProsAndConsViewModel(_dataProvider, _navigationService);

            prosAndConsViewModel.OpenDecidePromptCommand.Execute(null);

            Assert.IsTrue(prosAndConsViewModel.IsDecisionPopupVisible);
        }

        [Test]
        public void ShouldClosePopupWhenCancelDecision()
        {
            var testIssue = new Issue { Subject = "Test" };
            _dataProvider.LoadIssue(Arg.Any<int>()).Returns(testIssue);

            var prosAndConsViewModel = new ProsAndConsViewModel(_dataProvider, _navigationService);

            prosAndConsViewModel.OpenDecidePromptCommand.Execute(null);
            prosAndConsViewModel.CancelDecisionCommand.Execute(null);

            Assert.IsFalse(prosAndConsViewModel.IsDecisionPopupVisible);
        }

        [Test]
        public void ShouldSetIssueDecisionWhenDecided()
        {
            var testIssue = new Issue { Subject = "Test" };
            _dataProvider.LoadIssue(Arg.Any<int>()).Returns(testIssue);
            var prosAndConsViewModel = new ProsAndConsViewModel(_dataProvider, _navigationService);
            prosAndConsViewModel.IsDecisionPositive = true;

            prosAndConsViewModel.MakeDecisionCommand.Execute(null);

            Assert.AreEqual(Decision.Positive, testIssue.Decision);
        }

        [Test]
        public void ShouldApplyAllChangesToCurrentIssueWhenMakeDecision()
        {
            var testIssue = new Issue { Subject = "Test" };
            _dataProvider.LoadIssue(Arg.Any<int>()).Returns(testIssue);
            var prosAndConsViewModel = new ProsAndConsViewModel(_dataProvider, _navigationService);

            const string newSubject = "new subject";
            prosAndConsViewModel.Subject = newSubject;
            prosAndConsViewModel.ConsItems.Add(new EvaluationItem());
            prosAndConsViewModel.ProsItems.Add(new EvaluationItem());

            prosAndConsViewModel.MakeDecisionCommand.Execute(null);
            Assert.AreEqual(newSubject, testIssue.Subject);
            Assert.AreEqual(1, testIssue.PositivePoints.Count());
            Assert.AreEqual(1, testIssue.NegativePoints.Count());
        }

        [Test]
        public void ShouldSaveIssueWhenMakeDecision()
        {
            var testIssue = new Issue { Subject = "Test" };
            _dataProvider.LoadIssue(Arg.Any<int>()).Returns(testIssue);
            var prosAndConsViewModel = new ProsAndConsViewModel(_dataProvider, _navigationService);

            prosAndConsViewModel.MakeDecisionCommand.Execute(null);

            _dataProvider.Received().Save(testIssue);
        }

        [Test]
        public void ShouldNavigateToMainPageWhenDecided()
        {
            var testIssue = new Issue { Subject = "Test" };
            _dataProvider.LoadIssue(Arg.Any<int>()).Returns(testIssue);
            var prosAndConsViewModel = new ProsAndConsViewModel(_dataProvider, _navigationService);

            prosAndConsViewModel.MakeDecisionCommand.Execute(null);

            _navigationService.Received().Navigate(Arg.Is<Uri>(uri => uri.OriginalString.Contains("MainPage.xaml")));
        }
    }
}
