using System;

namespace ToBeOrNot.ViewModels
{
    public interface INavigationService
    {
        bool CanGoBack { get; }

        void Navigate(Uri sourcePage);

        void GoBack();
    }
}
