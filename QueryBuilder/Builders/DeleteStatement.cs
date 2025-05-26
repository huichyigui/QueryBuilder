using System.Text;

namespace QueryBuilder.Builders
{
    public class DeleteStatement : QueryStatementBase
    {
        public string From { get; set; }

        public string Build()
        {
            if (string.IsNullOrWhiteSpace(From)) throw new InvalidOperationException("FROM clause is missing.");
            
            StringBuilder sb = new StringBuilder();

            sb.Append($"DELETE FROM {From}");
            sb.Append(BuildWhere());

            return sb.ToString();
        }
    }
}
