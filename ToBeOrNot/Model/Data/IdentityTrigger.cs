using System.Linq;
using Wintellect.Sterling;
using Wintellect.Sterling.Database;

namespace ToBeOrNot.Model.Data
{
    public class IdentityTrigger<T> : BaseSterlingTrigger<T, int> where T : DataItem, new()
    {
        private int _key = 1;

        public IdentityTrigger(ISterlingDatabaseInstance database)
        {
            if (database.Query<T, int>().Any())
            {
                _key = database.Query<T, int>().Max(key => key.Key) + 1;
            }
        }

        public override bool BeforeSave(T instance)
        {
            if (instance.Key < 1)
            {
                instance.Key = _key++;
            }

            return true;
        }

        public override void AfterSave(T instance)
        {
        }

        public override bool BeforeDelete(int key)
        {
            return true;
        }
    }
}
