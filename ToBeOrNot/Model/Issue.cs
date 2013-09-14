using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ToBeOrNot.Model.Data;

namespace ToBeOrNot.Model
{
    public enum Decision
    {
        None,
        Negative,
        Positive
    }

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

        public Decision Decision { get; set; }

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

        public void ClearAllPoints()
        {
            _positivePoints.Clear();
            _negativePoints.Clear();
        }
    }
}
