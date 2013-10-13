using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ToBeOrNot.Model;
using ToBeOrNot.Model.Data;
using ToBeOrNot.ViewModels.Navigation;

namespace ToBeOrNot.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDataProvider _dataProvider;

        private RelayCommand _openAboutCommand;
        private RelayCommand _addNewIssueCommand;

        public MainViewModel(INavigationService navigationService, IDataProvider dataProvider)
        {
            _navigationService = navigationService;
            _dataProvider = dataProvider;
            CurrentIssues = new ObservableCollection<Issue>(_dataProvider.LoadCurrentIssues());
            DecidedIssues = new ObservableCollection<Issue>(_dataProvider.LoadDecidedIssues());
        }

        public ObservableCollection<Issue> CurrentIssues { get; private set; }

        public ObservableCollection<Issue> DecidedIssues { get; private set; }

        public ICommand OpenAboutCommand
        {
            get { return _openAboutCommand ?? (_openAboutCommand = new RelayCommand(OpenAboutPage)); }
        }

        public ICommand AddNewIssueCommand
        {
            get { return _addNewIssueCommand ?? (_addNewIssueCommand = new RelayCommand(AddNewIssue)); }
        }

        private void OpenAboutPage()
        {
            _navigationService.Navigate(new Uri("/Views/AboutPage.xaml", UriKind.Relative));
        }

        private void AddNewIssue()
        {
            _navigationService.Navigate(new Uri("/Views/NewIssuePage.xaml", UriKind.Relative));
        }
    }
}
