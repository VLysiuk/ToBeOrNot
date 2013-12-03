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
        private RelayCommand _deleteIssueCommand;
        private RelayCommand _navigatedToCommand;

        private Issue _selectedIssue;

        public MainViewModel(INavigationService navigationService, IDataProvider dataProvider)
        {
            _navigationService = navigationService;
            _dataProvider = dataProvider;
        }

        public ObservableCollection<Issue> CurrentIssues { get; private set; }

        public ObservableCollection<Issue> DecidedIssues { get; private set; }

        public Issue SelectedIssue
        {
            get
            {
                return _selectedIssue;
            }

            set
            {
                _selectedIssue = value;
                RaisePropertyChanged(() => SelectedIssue);
                if (_deleteIssueCommand != null)
                    _deleteIssueCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand OpenAboutCommand
        {
            get { return _openAboutCommand ?? (_openAboutCommand = new RelayCommand(OpenAboutPage)); }
        }

        public ICommand AddNewIssueCommand
        {
            get { return _addNewIssueCommand ?? (_addNewIssueCommand = new RelayCommand(AddNewIssue)); }
        }

        public ICommand DeleteIssueCommand
        {
            get { return _deleteIssueCommand ?? (_deleteIssueCommand = new RelayCommand(DeleteIssue, CanDeleteIssue)); }
        }

        public ICommand NavigatedToCommand
        {
            get { return _navigatedToCommand ?? (_navigatedToCommand = new RelayCommand(NavigatedTo)); }
        }

        private void OpenAboutPage()
        {
            _navigationService.Navigate(new Uri("/Views/AboutPage.xaml", UriKind.Relative));
        }

        private void AddNewIssue()
        {
            _navigationService.Navigate(new Uri("/Views/NewIssuePage.xaml", UriKind.Relative));
        }

        private void DeleteIssue()
        {
            _dataProvider.DeleteIssue(SelectedIssue);

            if (CurrentIssues.Contains(SelectedIssue))
                CurrentIssues.Remove(SelectedIssue);

            if (DecidedIssues.Contains(SelectedIssue))
                DecidedIssues.Remove(SelectedIssue);
        }

        private bool CanDeleteIssue()
        {
            return SelectedIssue != null;
        }

        private void NavigatedTo()
        {
            CurrentIssues = new ObservableCollection<Issue>(_dataProvider.LoadCurrentIssues());
            DecidedIssues = new ObservableCollection<Issue>(_dataProvider.LoadDecidedIssues());
        }
    }
}
