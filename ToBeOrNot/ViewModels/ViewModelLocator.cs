﻿using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using ToBeOrNot.Model;
using ToBeOrNot.Model.Data;
using ToBeOrNot.ViewModels.Navigation;

namespace ToBeOrNot.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<INavigationService>(() => new NavigationService());
            SimpleIoc.Default.Register<IAppSettingsProvider>(() => new AppSettingsProvider());
            SimpleIoc.Default.Register<ISpecialTasksProvider>(() => new PhoneSpecialTasksProvider());
            SimpleIoc.Default.Register<IDataProvider>(() => (App.Current as App).DataProvider);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AboutViewModel>();
            SimpleIoc.Default.Register<NewIssueViewModel>();
            SimpleIoc.Default.Register<ProsAndConsViewModel>();
        }

        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public AboutViewModel About
        {
            get { return ServiceLocator.Current.GetInstance<AboutViewModel>(); }
        }

        public NewIssueViewModel NewIssue
        {
            get { return ServiceLocator.Current.GetInstance<NewIssueViewModel>(); }
        }

        public ProsAndConsViewModel ProsAndCons
        {
            get { return ServiceLocator.Current.GetInstance<ProsAndConsViewModel>(); }
        }
    }
}
