namespace QueryBuilder.Models
{
    public interface ICondition
    {
        string ToSql();
    }
}
