using QueryBuilder.Enums;
using QueryBuilder.Models;
using System.Text;

namespace QueryBuilder.Builders
{
    public class UpdateStatement : QueryStatementBase<UpdateStatement>
    {
        private string _table { get; set; }
        private Dictionary<string, object> SetValues { get; set; } = new Dictionary<string, object>();

        public UpdateStatement For(string table)
        {
            if (string.IsNullOrWhiteSpace(table)) throw new ArgumentException("Table name cannot be null or empty.", nameof(table));
            _table = table;
            return this;
        }

        public UpdateStatement Set(string column, object value)
        {
            SetValues[column] = value;
            return this;
        }

        public string Build()
        {
            if (string.IsNullOrWhiteSpace(_table)) throw new InvalidOperationException("Table name is missing.");
            if (SetValues.Count == 0) throw new InvalidOperationException("No SET values provided.");
            
            StringBuilder sb = new StringBuilder();

            sb.Append($"UPDATE {_table} SET ");
            sb.Append(string.Join(", ", SetValues.Select(kv => $"{kv.Key} = {(kv.Value is string ? $"'{kv.Value}'" : kv.Value)}")));
            sb.Append(BuildWhere());

            return sb.ToString();
        }
    }
}
