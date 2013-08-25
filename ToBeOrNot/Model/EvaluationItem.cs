namespace ToBeOrNot.Model
{
    public enum ItemValue
    {
        Small,
        Normal,
        Big
    }

    public class EvaluationItem
    {
        public string Name { get; set; }

        public ItemValue Value { get; set; }
    }
}
