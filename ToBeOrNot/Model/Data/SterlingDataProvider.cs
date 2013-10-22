using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.Sterling;
using Wintellect.Sterling.IsolatedStorage;

namespace ToBeOrNot.Model.Data
{
    public class SterlingDataProvider : IDataProvider
    {
        private SterlingEngine _engine;
        private ISterlingDatabaseInstance _database;
        private SterlingDefaultLogger _logger;

        public void SetupDatabase()
        {
            _engine = new SterlingEngine();

            _logger = new SterlingDefaultLogger(SterlingLogLevel.Verbose);

            _engine.Activate();

            // by default Sterling DB works in memory but we want it to persist to IsolatedStorage
            _database = _engine.SterlingDatabase.RegisterDatabase<IssuesDatabase>(new IsolatedStorageDriver());

            _database.RegisterTrigger(new IdentityTrigger<Issue>(_database));
        }

        public void CleanUpDatabase()
        {
            _database.Flush();
            _logger.Detach();
            _engine.Dispose();
            _database = null;
            _engine = null;
        }

        public void Save(Issue issue)
        {
            _database.Save(issue);
            _database.Flush();
        }

        public Issue LoadIssue(int issueKey)
        {
            var tableKey = _database.Query<Issue, int>().FirstOrDefault(i => i.Key == issueKey);
            
            if (tableKey == null)
                return null;

            return tableKey.LazyValue.Value;
        }

        public IEnumerable<Issue> LoadCurrentIssues()
        {
            return _database.Query<Issue, int>()
                            .Where(i => i.LazyValue.Value.Decision == Decision.None)
                            .Select(i => i.LazyValue.Value);
        }

        public IEnumerable<Issue> LoadDecidedIssues()
        {
            return _database.Query<Issue, int>()
                            .Where(i => i.LazyValue.Value.Decision != Decision.None)
                            .Select(i => i.LazyValue.Value);
        }

        public void DeleteIssue(Issue issue)
        {
            _database.Delete(issue);
        }
    }
}
