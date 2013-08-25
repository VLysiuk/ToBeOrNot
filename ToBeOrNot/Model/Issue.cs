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
        public Issue()
        {
        }

        public Issue(string subject)
        {
            Subject = subject;
            PositivePoints = new List<EvaluationItem>();
            NegativePoints = new List<EvaluationItem>();
        }

        public string Subject { get; set; }

        public BitmapImage Image { get; set; }

        public bool IsDecided { get; set; }

        public IEnumerable<EvaluationItem> PositivePoints { get; private set; }

        public IEnumerable<EvaluationItem> NegativePoints { get; private set; }
    }
}
