using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ToBeOrNot.Model;

namespace ToBeOrNot.ViewModels
{
    public class NewIssueViewModel : ViewModelBase
    {
        private readonly ISpecialTasksProvider _specialTasksProvider;
        private RelayCommand _selectPictureCommand;
        private RelayCommand _takePhotoCommand;
        private BitmapImage _selectedImage;

        public NewIssueViewModel(ISpecialTasksProvider specialTasksProvider)
        {
            _specialTasksProvider = specialTasksProvider;
            _specialTasksProvider.PictureSelected += (s, args) => { SelectedImage = args.SelectedPicture; };
        }

        public BitmapImage SelectedImage
        {
            get
            {
                return _selectedImage;
            }

            set 
            { 
                _selectedImage = value;
                RaisePropertyChanged(() => SelectedImage);
            }
        }

        public ICommand SelectPictureCommand
        {
            get { return _selectPictureCommand ?? (_selectPictureCommand = new RelayCommand(SelectPicture)); }
        }

        public ICommand TakePhotoCommand
        {
            get { return _takePhotoCommand ?? (_takePhotoCommand = new RelayCommand(TakePhoto)); }
        }

        private void SelectPicture()
        {
            _specialTasksProvider.ChoosePicture();
        }

        private void TakePhoto()
        {
            _specialTasksProvider.TakePhoto();
        }
    }
}
