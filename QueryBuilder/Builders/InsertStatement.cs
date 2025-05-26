using System.Text;

namespace QueryBuilder.Builders
{
    public class InsertStatement : QueryStatementBase
    {
        public string Into { get; set; } // table name to insert into
        private Dictionary<string, object> _values { get; set; } = new Dictionary<string, object>();

        public InsertStatement Values(Dictionary<string, object> values)
        {
            foreach (var kv in values)
                _values[kv.Key] = kv.Value;
            return this;
        }

        public string Build()
        {
            if (string.IsNullOrWhiteSpace(Into)) throw new InvalidOperationException("Table name is missing.");
            if (_values.Count == 0) throw new InvalidOperationException("No values provided.");

            var columns = string.Join(", ", _values.Keys);
            var parameters = string.Join(", ", _values.Values.Select(v =>
                v is string ? $"'{v}'" : v.ToString()));

            return $"INSERT INTO {Into} ({columns}) VALUES ({parameters})";
        }
    }
}
