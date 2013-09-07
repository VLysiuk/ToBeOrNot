using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ToBeOrNot.Model;

namespace ToBeOrNot.Test
{
    [TestFixture]
    public class IssueTests
    {
        [Test]
        public void ShouldAddPositivePointsToList()
        {
            var issue = new Issue("test");
            var positivePoint = new EvaluationItem {Name = "price", Value = ItemValue.Big};

            issue.AddPositivePoint(positivePoint);

            Assert.IsTrue(issue.PositivePoints.Count() == 1);
            Assert.IsTrue(issue.PositivePoints.Any(i => i.Name == "price"));
        }

        [Test]
        public void ShouldAddNegativePointsToList()
        {
            var issue = new Issue("test");
            var negativePoint = new EvaluationItem { Name = "price", Value = ItemValue.Big };

            issue.AddNegativePoint(negativePoint);

            Assert.IsTrue(issue.NegativePoints.Count() == 1);
            Assert.IsTrue(issue.NegativePoints.Any(i => i.Name == "price"));
        }
    }
}
