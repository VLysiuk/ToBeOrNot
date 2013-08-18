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
    public class NewIssueViewModelTests
    {
        [Test]
        public void ShouldPerformPhotoChooserTask()
        {
            var tasksProvider = Substitute.For<ISpecialTasksProvider>();
            var newIssueViewModel = new NewIssueViewModel(tasksProvider);
            
            newIssueViewModel.SelectPictureCommand.Execute(null);

            tasksProvider.Received().ChoosePicture();
        }

        [Test]
        public void ShouldPerformTakePhotoTask()
        {
            var tasksProvider = Substitute.For<ISpecialTasksProvider>();
            var newIssueViewModel = new NewIssueViewModel(tasksProvider);

            newIssueViewModel.TakePhotoCommand.Execute(null);

            tasksProvider.Received().TakePhoto();
        }
    }
}
