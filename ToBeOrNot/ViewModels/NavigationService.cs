using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ToBeOrNot.ViewModels
{
    public class NavigationService : INavigationService
    {
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

        public void GoBack()
        {
            RootFrame.GoBack();
        }
    }
}
