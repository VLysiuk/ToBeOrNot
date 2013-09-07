using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ToBeOrNot.Model.Data;

namespace ToBeOrNot.Model
{
    public class Issue : DataItem
    {
        private List<EvaluationItem> _positivePoints;

        private List<EvaluationItem> _negativePoints;

        public Issue()
        {
            _positivePoints = new List<EvaluationItem>();
            _negativePoints = new List<EvaluationItem>();
        }

        public Issue(string subject)
        {
            Subject = subject;
            _positivePoints = new List<EvaluationItem>();
            _negativePoints = new List<EvaluationItem>();
        }

        public string Subject { get; set; }

        public BitmapImage Image { get; set; }

        public bool IsDecided { get; set; }

        public IEnumerable<EvaluationItem> PositivePoints
        {
            get { return _positivePoints; }
        }

        public IEnumerable<EvaluationItem> NegativePoints
        {
            get { return _negativePoints; }
        }

        public void AddPositivePoint(EvaluationItem evaluationItem)
        {
            _positivePoints.Add(evaluationItem);
        }

        public void AddNegativePoint(EvaluationItem evaluationItem)
        {
            _negativePoints.Add(evaluationItem);
        }
    }
}
