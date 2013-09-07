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
            var testIssue = new Issue { Subject = "Test" };
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
            var testIssue = new Issue {Subject = "Test"};
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
    }
}
