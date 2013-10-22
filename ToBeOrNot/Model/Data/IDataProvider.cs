using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToBeOrNot.Model.Data
{
    public interface IDataProvider
    {
        void SetupDatabase();

        void CleanUpDatabase();

        void Save(Issue issue);

        Issue LoadIssue(int issueKey);

        IEnumerable<Issue> LoadCurrentIssues();
        
        IEnumerable<Issue> LoadDecidedIssues();

        void DeleteIssue(Issue issue);
    }
}
