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
        private RelayCommand _rateAndReviewCommand;
        private RelayCommand _writeAnEmailCommand;

        public AboutViewModel(IAppSettingsProvider appSettingsProvider)
        {
            _appSettingsProvider = appSettingsProvider;
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

        private static void RateAndReview()
        {
            var task = new MarketplaceReviewTask();
            task.Show();
        }

        private void WriteAnEmail()
        {
            var task = new EmailComposeTask();
            task.To = _appSettingsProvider.GetAuthorEmail();
            task.Show();
        }
    }
}
