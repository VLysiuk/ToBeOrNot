using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using ToBeOrNot.Model;

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
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AboutViewModel>();
            SimpleIoc.Default.Register<NewIssueViewModel>();
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
    }
}
