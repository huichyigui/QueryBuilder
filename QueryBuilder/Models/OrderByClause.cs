using QueryBuilder.Enums;

namespace QueryBuilder.Models
{
    public class OrderByClause
    {
        public string Column { get; set; }
        public SqlOrderBy Order { get; set; } = SqlOrderBy.Asc;

        public override string ToString()
        {
            return $"{Column} {Order.ToString().ToUpper()}";
        }
    }
}
