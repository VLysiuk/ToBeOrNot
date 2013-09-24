using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        private RelayCommand _makeDecisionCommand;
        private RelayCommand _decideLaterCommand;
        private RelayCommand _openDecidePromptCommand;
        private RelayCommand _cancelDecisionCommand;
        private RelayCommand _addProsConsItemCommand;
        private RelayCommand _cancelAddItemCommand;
        private RelayCommand _openAddPromptCommand;
        private bool _isDecisionPopupVisible;
        private bool _isDecisionPositive;
        private bool _isProsItem;
        private string _prosAndConsItemText;
        private bool _isAddPopupVisible;
        private ItemValue _prosAndConsValue;

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

        public bool IsDecisionPopupVisible
        {
            get
            {
                return _isDecisionPopupVisible;
            }

            set
            {
                _isDecisionPopupVisible = value;
                RaisePropertyChanged(() => IsDecisionPopupVisible);
            }
        }

        public bool IsDecisionPositive
        {
            get
            {
                return _isDecisionPositive;
            }

            set
            {
                _isDecisionPositive = value;
                RaisePropertyChanged(() => IsDecisionPositive);
                RaisePropertyChanged(() => DecisionText);
            }
        }

        public string DecisionText
        {
            get { return IsDecisionPositive ? Resources.AppResources.YesText : Resources.AppResources.NoText; }
        }

        public bool IsAddPopupVisible
        {
            get
            {
                return _isAddPopupVisible;
            }

            set
            {
                _isAddPopupVisible = value;
                RaisePropertyChanged(() => IsAddPopupVisible);
            }
        }

        public bool IsProsItem
        {
            get 
            {
                return _isProsItem;
            }

            set 
            { 
                _isProsItem = value;
                RaisePropertyChanged(() => IsProsItem);
                RaisePropertyChanged(() => EvaluationValueText);
            }
        }

        public string EvaluationValueText
        {
            get { return IsProsItem ? Resources.AppResources.PositiveText : Resources.AppResources.NegativeText; }
        }

        public string ProsAndConsItemText
        {
            get
            {
                return _prosAndConsItemText;
            }

            set 
            {
                _prosAndConsItemText = value;
                RaisePropertyChanged(() => ProsAndConsItemText);
            }
        }

        public ItemValue ProsAndConsValue
        {
            get
            {
                return _prosAndConsValue;
            }

            set
            {
                _prosAndConsValue = value;
                RaisePropertyChanged(() => ProsAndConsValue);
            }
        }

        public ICommand MakeDecisionCommand
        {
            get { return _makeDecisionCommand ?? (_makeDecisionCommand = new RelayCommand(MakeDecision)); }
        }

        public ICommand DecideLaterCommand
        {
            get { return _decideLaterCommand ?? (_decideLaterCommand = new RelayCommand(DecideLater)); }
        }

        public ICommand OpenDecidePromptCommand
        {
            get { return _openDecidePromptCommand ?? (_openDecidePromptCommand = new RelayCommand(OpenDecidePrompt)); }
        }

        public ICommand CancelDecisionCommand
        {
            get { return _cancelDecisionCommand ?? (_cancelDecisionCommand = new RelayCommand(CancelDecision)); }
        }

        public ICommand OpenAddPromptCommand
        {
            get { return _openAddPromptCommand ?? (_openAddPromptCommand = new RelayCommand(OpenAddPrompt)); }
        }

        public ICommand AddProsConsItemCommand
        {
            get
            {
                return _addProsConsItemCommand ??
                       (_addProsConsItemCommand = new RelayCommand(AddProsConsItem, CanAddProsConsItem));
            }
        }

        public ICommand CancelAddItemCommand
        {
            get { return _cancelAddItemCommand ?? (_cancelAddItemCommand = new RelayCommand(CancelAddItem)); }
        }

        private void MakeDecision()
        {
            _currentIssue.Decision = IsDecisionPositive ? Decision.Positive : Decision.Negative;
            ApplyChanges();
            _dataProvider.Save(_currentIssue);
            _navigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void DecideLater()
        {
            ApplyChanges();
            _dataProvider.Save(_currentIssue);
            _navigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void ApplyChanges()
        {
            _currentIssue.Subject = Subject;
            _currentIssue.ClearAllPoints();

            foreach (var prosItem in _prosItems)
            {
                _currentIssue.AddPositivePoint(prosItem);
            }

            foreach (var consItem in _consItems)
            {
                _currentIssue.AddNegativePoint(consItem);
            }
        }

        private void OpenDecidePrompt()
        {
            IsDecisionPopupVisible = true;
        }

        private void CancelDecision()
        {
            IsDecisionPopupVisible = false;
        }

        private void OpenAddPrompt()
        {
            IsAddPopupVisible = true;
        }

        private void AddProsConsItem()
        {
            var evaluationItem = new EvaluationItem {Name = ProsAndConsItemText, Value = ProsAndConsValue};
            if (IsProsItem)
                ProsItems.Add(evaluationItem);
            else
                ConsItems.Add(evaluationItem);

            ResetProsAndConsValues();
        }

        private bool CanAddProsConsItem()
        {
            return !string.IsNullOrEmpty(ProsAndConsItemText);
        }

        private void CancelAddItem()
        {
            IsAddPopupVisible = false;
            ResetProsAndConsValues();
        }

        private void ResetProsAndConsValues()
        {
            ProsAndConsItemText = string.Empty;
            IsProsItem = false;
            ProsAndConsValue = ItemValue.Normal;
        }
    }
}
