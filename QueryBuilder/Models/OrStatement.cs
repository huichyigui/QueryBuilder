namespace QueryBuilder.Models
{
    public class OrStatement : ICondition
    {
        private readonly List<ICondition> _conditions;

        public OrStatement(params ICondition[] conditions)
        {
            _conditions = conditions.ToList();
        }

        public string ToSql()
        {
            return "(" + string.Join(" OR ", _conditions.Select(c => c.ToSql())) + ")";
        }
    }
}
