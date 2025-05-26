using QueryBuilder.Enums;

namespace QueryBuilder.Models
{
    public class WhereClause : ICondition
    {
        public string Column { get; set; }
        public SqlOperator Operator { get; set; }

        private object? _value;
        public object? Value 
        {
            get => _value; 
            set
            {
                if ((Operator != SqlOperator.IsNull && Operator != SqlOperator.IsNotNull) && value == null)
                    throw new ArgumentNullException(nameof(value), $"Operator '{Operator}' on column '{Column}' requires a non-null value.");
                _value = value;
            }
        }

        public WhereClause(string column, SqlOperator op, object? value)
        {
            Column = column;
            Operator = op;
            Value = value;
        }

        public string ToSql()
        {
            return Operator switch
            { 
                SqlOperator.IsNull => $"{Column} IS NULL",
                SqlOperator.IsNotNull => $"{Column} IS NOT NULL",
                SqlOperator.In => $"{Column} {GetOperatorString()} ({FormatInValues()})",
                _ => $"{Column} {GetOperatorString()} {(Value is string ? $"'{Value}'" : Value)}"
            };
        }

        private string GetOperatorString()
        {
            return Operator switch
            {
                SqlOperator.Equal => "=",
                SqlOperator.NotEqual => "<>",
                SqlOperator.GreaterThan => ">",
                SqlOperator.LessThan => "<",
                SqlOperator.GreaterThanOrEqual => ">=",
                SqlOperator.LessThanOrEqual => "<=",
                SqlOperator.Like => "LIKE",
                SqlOperator.In => "IN",
                SqlOperator.NotIn => "NOT IN",
                _ => throw new NotSupportedException($"Operator {Operator} is not supported.")
            };
        }

        private object FormatInValues()
        {
            if (Value is IEnumerable<object> values)
                return string.Join(", ", values.Select(v => v is string ? $"'{v}'" : v.ToString()));
            else
                return Value;
        }
    }
}
