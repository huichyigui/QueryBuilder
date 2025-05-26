using System.Text;

namespace QueryBuilder.Builders
{
    public class InsertStatement : QueryStatementBase<InsertStatement>
    {
        private string _into { get; set; } // table name to insert into
        private Dictionary<string, object> _values { get; set; } = new Dictionary<string, object>();

        public InsertStatement Into(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentException("Table name cannot be null or empty.", nameof(tableName));
            _into = tableName;
            return this;
        }

        public InsertStatement Values(Dictionary<string, object> values)
        {
            foreach (var kv in values)
                _values[kv.Key] = kv.Value;
            return this;
        }

        public string Build()
        {
            if (string.IsNullOrWhiteSpace(_into)) throw new InvalidOperationException("Table name is missing.");
            if (_values.Count == 0) throw new InvalidOperationException("No values provided.");

            var columns = string.Join(", ", _values.Keys);
            var parameters = string.Join(", ", _values.Values.Select(v =>
                v is string ? $"'{v}'" : v.ToString()));

            return $"INSERT INTO {_into} ({columns}) VALUES ({parameters})";
        }
    }
}
