using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using ToBeOrNot.Model;
using ToBeOrNot.Model.Data;

namespace ToBeOrNot.ViewModels
{
    public class ProsAndConsViewModel : ViewModelBase
    {
        private readonly IDataProvider _dataProvider;
        private string _subject;
        private ObservableCollection<EvaluationItem> _prosItems;
        private ObservableCollection<EvaluationItem> _consItems;

        public ProsAndConsViewModel(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
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
