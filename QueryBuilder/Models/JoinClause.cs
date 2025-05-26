using QueryBuilder.Enums;

namespace QueryBuilder.Models
{
    public class JoinClause
    {
        public required string Table { get; set; }
        public required SqlJoin JoinType { get; set; }
        public required string OnCondition { get; set; }

        public override string ToString()
        {
            string keyword = JoinType switch
            {
                SqlJoin.Inner => "INNER JOIN",
                SqlJoin.Left => "LEFT JOIN",
                SqlJoin.Right => "RIGHT JOIN",
                _ => throw new NotSupportedException($"Join type {JoinType} is not supported.")
            };
            return $"{keyword} {Table} ON {OnCondition}";
        }
    }
}
