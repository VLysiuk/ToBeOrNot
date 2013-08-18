using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ToBeOrNot.Misc
{
    public class SelectedPictureEventArgs : EventArgs
    {
        public SelectedPictureEventArgs(BitmapImage image)
        {
            SelectedPicture = image;
        }

        public BitmapImage SelectedPicture { get; private set; }
    }
}
