using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using ToBeOrNot.Model;
using ToBeOrNot.Model.Data;
using ToBeOrNot.ViewModels.Navigation;

namespace ToBeOrNot.ViewModels
{
    public class ProsAndConsViewModel : ViewModelBase
    {
        private readonly IDataProvider _dataProvider;
        private readonly INavigationService _navigationService;
        private readonly Issue _currentIssue;
        private string _subject;
        private ObservableCollection<EvaluationItem> _prosItems;
        private ObservableCollection<EvaluationItem> _consItems;

        public ProsAndConsViewModel(IDataProvider dataProvider, INavigationService navigationService)
        {
            _dataProvider = dataProvider;
            _navigationService = navigationService;
            var issueKey = _navigationService.GetNavigationParameter<int>("issueKey");
            _currentIssue = _dataProvider.LoadIssue(issueKey);
            Subject = _currentIssue.Subject;
            ProsItems = new ObservableCollection<EvaluationItem>(_currentIssue.PositivePoints);
            ConsItems = new ObservableCollection<EvaluationItem>(_currentIssue.NegativePoints);
        }

        public string Subject
        {
            get
            {
                return _subject;
            }

            set
            {
                _subject = value;
                RaisePropertyChanged(() => Subject);
            }
        }

        public ObservableCollection<EvaluationItem> ProsItems
        {
            get
            {
                return _prosItems;
            }

            set
            {
                _prosItems = value;
                RaisePropertyChanged(() => ProsItems);
            }
        }

        public ObservableCollection<EvaluationItem> ConsItems
        {
            get
            {
                return _consItems;
            }

            set
            {
                _consItems = value;
                RaisePropertyChanged(() => ConsItems);
            }
        }
    }
}
