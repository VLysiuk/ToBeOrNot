using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Tasks;
using ToBeOrNot.Model;
using ToBeOrNot.Resources;

namespace ToBeOrNot.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly ISpecialTasksProvider _specialTasksProvider;
        private RelayCommand _rateAndReviewCommand;
        private RelayCommand _writeAnEmailCommand;

        public AboutViewModel(IAppSettingsProvider appSettingsProvider, ISpecialTasksProvider specialTasksProvider)
        {
            _appSettingsProvider = appSettingsProvider;
            _specialTasksProvider = specialTasksProvider;
        }

        public string Version
        {
            get { return string.Concat(AppResources.AppVersionText, _appSettingsProvider.GetAppVersion()); }
        }

        public ICommand RateAndReviewCommand
        {
            get { return _rateAndReviewCommand ?? (_rateAndReviewCommand = new RelayCommand(RateAndReview)); }
        }

        public ICommand WriteAnEmailCommand
        {
            get { return _writeAnEmailCommand ?? (_writeAnEmailCommand = new RelayCommand(WriteAnEmail)); }
        }

        private void RateAndReview()
        {
            _specialTasksProvider.RateAndReview();
        }

        private void WriteAnEmail()
        {
            _specialTasksProvider.ComposeEmail(_appSettingsProvider.GetAuthorEmail());
        }
    }
}
