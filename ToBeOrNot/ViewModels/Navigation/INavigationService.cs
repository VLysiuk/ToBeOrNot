using System;
using System.Collections.Generic;

namespace ToBeOrNot.ViewModels.Navigation
{
    public interface INavigationService
    {
        bool CanGoBack { get; }

        void Navigate(Uri sourcePage);

        void Navigate(Uri sourcePage, Dictionary<string, object> parameters);

        T GetNavigationParameter<T>(string parameterName);

        void GoBack();
    }
}
