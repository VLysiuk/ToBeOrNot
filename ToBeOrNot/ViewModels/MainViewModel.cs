using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ToBeOrNot.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private RelayCommand _openAboutCommand;
        
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand OpenAboutCommand
        {
            get { return _openAboutCommand ?? (_openAboutCommand = new RelayCommand(OpenAboutPage)); }
        }

        private void OpenAboutPage()
        {
            _navigationService.Navigate(new Uri("/Views/AboutPage.xaml", UriKind.Relative));
        }
    }
}
