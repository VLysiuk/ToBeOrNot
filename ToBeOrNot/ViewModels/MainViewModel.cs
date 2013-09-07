﻿using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ToBeOrNot.ViewModels.Navigation;

namespace ToBeOrNot.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private RelayCommand _openAboutCommand;
        private RelayCommand _addNewIssueCommand;
        
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

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
