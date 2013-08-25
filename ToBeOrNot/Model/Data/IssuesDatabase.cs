using System.Collections.Generic;
using Wintellect.Sterling.Database;

namespace ToBeOrNot.Model.Data
{
    public class IssuesDatabase : BaseDatabaseInstance
    {
        private const string DatabaseName = "IssuesDatabase";

        public override string Name
        {
            get { return DatabaseName; }
        }

        protected override List<ITableDefinition> RegisterTables()
        {
            return new List<ITableDefinition>
                {
                    CreateTableDefinition<Issue, int>(dataItem => dataItem.Key)
                };
        }
    }
}
