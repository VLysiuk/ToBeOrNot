using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBeOrNot.Misc;

namespace ToBeOrNot.Model
{
    public interface ISpecialTasksProvider
    {
        event EventHandler<SelectedPictureEventArgs> PictureSelected;

        void RateAndReview();

        void ComposeEmail(string recipient);

        void ChoosePicture();
        
        void TakePhoto();
    }
}
