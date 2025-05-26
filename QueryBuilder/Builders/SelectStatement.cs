using QueryBuilder.Enums;
using QueryBuilder.Models;
using System.Text;

namespace QueryBuilder.Builders
{
    public class SelectStatement : QueryStatementBase<SelectStatement>
    {
        private readonly List<OrderByClause> _orderByClauses = new();

        private string _select { get; set; } = "*";
        private int _top { get; set; } = 0;
        private bool _distinct { get; set; }
        private string _from { get; set; }
        private string _groupBy { get; set; }

        public SelectStatement Select(string columns)
        {
            if (string.IsNullOrWhiteSpace(columns)) throw new ArgumentException("Columns cannot be null or empty.", nameof(columns));
            _select = columns;
            return this;
        }

        public SelectStatement Top(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "TOP value must be greater than 0.");

            _top = value;
            return this;
        }

        public SelectStatement Distinct(bool distinct = true)
        {
            _distinct = distinct;
            return this;
        }

        public SelectStatement From(string table)
        {
            if (string.IsNullOrWhiteSpace(table)) throw new ArgumentException("Table name cannot be null or empty.", nameof(table));
            _from = table;
            return this;
        }

        public SelectStatement OrderBy(string column)
        {
            return OrderBy(column, SqlOrderBy.Asc);
        }

        public SelectStatement OrderBy(string column, SqlOrderBy order)
        {
            _orderByClauses.Add(new OrderByClause
            {
                Column = column,
                Order = order
            });
            return this;
        }

        public SelectStatement GroupBy(string columns)
        {
            if (string.IsNullOrWhiteSpace(columns)) throw new ArgumentException("Group by columns cannot be null or empty.", nameof(columns));

            _groupBy = columns;
            return this;
        }

        public string Build()
        {
            if (string.IsNullOrWhiteSpace(_select)) throw new InvalidOperationException("SELECT clause is missing.");
            if (string.IsNullOrWhiteSpace(_from)) throw new InvalidOperationException("FROM clause is missing.");

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT ");

            if (_distinct)
                sb.Append("DISTINCT ");

            if (_top > 0)
                sb.Append($"TOP {_top} ");

            sb.Append(_select);
            sb.Append($" FROM {_from}");
            sb.Append(BuildJoins());
            sb.Append(BuildWhere());

            if (!string.IsNullOrWhiteSpace(_groupBy))
                sb.Append($" GROUP BY {_groupBy}");

            if (_orderByClauses.Any())
                sb.Append(" ORDER BY ").Append(string.Join(", ", _orderByClauses));

            return sb.ToString();
        }
    }
}
