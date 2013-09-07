using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ToBeOrNot.ViewModels.Navigation
{
    public class NavigationService : INavigationService
    {
        private Dictionary<string, object> _navigationContext;

        public bool CanGoBack
        {
            get { return RootFrame.CanGoBack; }
        }

        private Frame RootFrame
        {
            get { return Application.Current.RootVisual as Frame; }
        } 

        public void Navigate(Uri sourcePage)
        {
            RootFrame.Navigate(sourcePage);
        }

        public void Navigate(Uri sourcePage, Dictionary<string, object> parameters)
        {
            RootFrame.Navigate(sourcePage);
            _navigationContext.Clear();
            _navigationContext = parameters;
        }

        public T GetNavigationParameter<T>(string parameterName)
        {
            object value;
            _navigationContext.TryGetValue(parameterName, out value);
            return (T)value;
        }

        public void GoBack()
        {
            RootFrame.GoBack();
        }
    }
}
