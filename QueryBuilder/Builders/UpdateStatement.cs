using QueryBuilder.Enums;
using QueryBuilder.Models;
using System.Text;

namespace QueryBuilder.Builders
{
    public class UpdateStatement : QueryStatementBase
    {
        public string Table { get; set; }
        public Dictionary<string, object> SetValues { get; set; } = new Dictionary<string, object>();
        public WhereClause? WhereClause { get; set; }

        public UpdateStatement Set(string column, object value)
        {
            SetValues[column] = value;
            return this;
        }

        public string Build()
        {
            if (string.IsNullOrWhiteSpace(Table)) throw new InvalidOperationException("Table name is missing.");
            if (SetValues.Count == 0) throw new InvalidOperationException("No SET values provided.");
            
            StringBuilder sb = new StringBuilder();

            sb.Append($"UPDATE {Table} SET ");
            sb.Append(string.Join(", ", SetValues.Select(kv => $"{kv.Key} = {(kv.Value is string ? $"'{kv.Value}'" : kv.Value)}")));
            sb.Append(BuildWhere());

            return sb.ToString();
        }
    }
}
