using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ToBeOrNot.Model;
using ToBeOrNot.Model.Data;
using ToBeOrNot.ViewModels.Navigation;
using ToBeOrNot.Views;

namespace ToBeOrNot.ViewModels
{
    public class NewIssueViewModel : ViewModelBase
    {
        private readonly ISpecialTasksProvider _specialTasksProvider;
        private readonly IDataProvider _dataProvider;
        private readonly INavigationService _navigationService;
        private RelayCommand _selectPictureCommand;
        private RelayCommand _takePhotoCommand;
        private RelayCommand _saveIssueCommand;
        private RelayCommand _cancelIssueCommand;
        private RelayCommand _navigatedToCommand;
        private string _subject;
        private BitmapImage _selectedImage;

        public NewIssueViewModel(ISpecialTasksProvider specialTasksProvider, IDataProvider dataProvider, INavigationService navigationService)
        {
            _specialTasksProvider = specialTasksProvider;
            _dataProvider = dataProvider;
            _navigationService = navigationService;
            _specialTasksProvider.PictureSelected += (s, args) => { SelectedImage = args.SelectedPicture; };
        }

        public string Subject
        {
            get
            {
                return _subject;
            }

            set
            {
                _subject = value;
                RaisePropertyChanged(() => Subject);
                if (_saveIssueCommand != null)
                    _saveIssueCommand.RaiseCanExecuteChanged();
            }
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

        public ICommand SaveIssueCommand
        {
            get { return _saveIssueCommand ?? (_saveIssueCommand = new RelayCommand(SaveIssue, CanSaveIssue)); }
        }

        public ICommand CancelIssueCommand
        {
            get { return _cancelIssueCommand ?? (_cancelIssueCommand = new RelayCommand(CancelIssue)); }
        }

        public ICommand NavigatedToCommand
        {
            get { return _navigatedToCommand ?? (_navigatedToCommand = new RelayCommand(NavigatedTo)); }
        }

        private void SelectPicture()
        {
            _specialTasksProvider.ChoosePicture();
        }

        private void TakePhoto()
        {
            _specialTasksProvider.TakePhoto();
        }

        private void SaveIssue()
        {
            var newIssue = new Issue(_subject);
            _dataProvider.Save(newIssue);
            _navigationService.Navigate(
                new Uri("/Views/ProsAndConsPage.xaml", UriKind.Relative),
                new Dictionary<string, object> { { "issueKey", newIssue.Key } });
        }

        private bool CanSaveIssue()
        {
            return !string.IsNullOrWhiteSpace(_subject);
        }

        private void CancelIssue()
        {
            _navigationService.GoBack();
        }

        private void NavigatedTo()
        {
            Subject = string.Empty;
            SelectedImage = null;
        }
    }
}
