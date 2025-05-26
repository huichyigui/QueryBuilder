using QueryBuilder.Enums;
using QueryBuilder.Models;
using System.Text;

namespace QueryBuilder.Builders
{
    public class SelectStatement : QueryStatementBase
    {
        public string Select { get; set; } = "*";

        private int _top;
        public int Top 
        {
            get => _top; 
            set
            {
                if (value < 0 || value == 0) throw new ArgumentOutOfRangeException(nameof(value), "TOP value must be greater than 0.");

                _top = value;
            }
        }

        public bool Distinct { get; set; } = false;
        public string From { get; set; }
        public SqlOrderBy OrderBy { get; set; } = SqlOrderBy.Asc;
        public string OrderByColumn { get; set; }
        public string? GroupBy { get; set; }

        public SelectStatement Where(string column, SqlOperator op, object value)
        {
            var condition = new WhereClause
            {
                Column = column,
                Operator = op,
                Value = value
            };
            _where = condition;
            return this;
        } 

        public string Build()
        {
            if (string.IsNullOrWhiteSpace(Select)) throw new InvalidOperationException("SELECT clause is missing.");
            if (string.IsNullOrWhiteSpace(From)) throw new InvalidOperationException("FROM clause is missing.");

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT ");

            if (Distinct)
                sb.Append("DISTINCT ");

            if (Top > 0)
                sb.Append($"TOP {Top} ");

            sb.Append(Select);
            sb.Append($" FROM {From}");
            sb.Append(BuildJoins());
            sb.Append(BuildWhere());

            if (!string.IsNullOrWhiteSpace(OrderByColumn))
                sb.Append($" ORDER BY {OrderByColumn} {OrderBy.ToString().ToUpper()}");

            if (!string.IsNullOrWhiteSpace(GroupBy))
                sb.Append($" GROUP BY {GroupBy}");

            return sb.ToString();
        }
    }
}
