using System.Text;

namespace QueryBuilder.Builders
{
    public class DeleteStatement : QueryStatementBase<DeleteStatement>
    {
        private string _from { get; set; }

        public DeleteStatement From(string table)
        {
            if (string.IsNullOrWhiteSpace(table)) throw new ArgumentException("Table name cannot be null or empty.", nameof(table));
            _from = table;
            return this;
        }

        public string Build()
        {
            if (string.IsNullOrWhiteSpace(_from)) throw new InvalidOperationException("FROM clause is missing.");
            
            StringBuilder sb = new StringBuilder();

            sb.Append($"DELETE FROM {_from}");
            sb.Append(BuildWhere());

            return sb.ToString();
        }
    }
}
