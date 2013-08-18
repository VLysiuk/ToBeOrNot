using System;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using ToBeOrNot.Misc;

namespace ToBeOrNot.Model
{
    public class PhoneSpecialTasksProvider : ISpecialTasksProvider
    {
        public event EventHandler<SelectedPictureEventArgs> PictureSelected = delegate { }; 

        public void RateAndReview()
        {
            var task = new MarketplaceReviewTask();
            task.Show();
        }

        public void ComposeEmail(string recipient)
        {
            var task = new EmailComposeTask();
            task.To = recipient;
            task.Show();
        }

        public void ChoosePicture()
        {
            var photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += PictureSelectedHandler;
            photoChooserTask.Show();
        }

        public void TakePhoto()
        {
            var cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed += PictureSelectedHandler;
            cameraCaptureTask.Show();
        }

        private void PictureSelectedHandler(object sender, PhotoResult e)
        {
            if (null != e.ChosenPhoto && e.TaskResult == TaskResult.OK)
            {
                var image = new BitmapImage();
                image.SetSource(e.ChosenPhoto);
                PictureSelected(this, new SelectedPictureEventArgs(image));
            }
        }
    }
}
