using QueryBuilder.Enums;
using QueryBuilder.Models;
using System.Text;

namespace QueryBuilder.Builders
{
    public abstract class QueryStatementBase<T> where T : QueryStatementBase<T>
    {
        protected ICondition _where;
        protected readonly List<JoinClause> _joins = new();

        public T Where(string column, SqlOperator op)
        {
            return Where(column, op, null);
        }

        public T Where(string column, SqlOperator op, object value)
        {
            var newCondition = new WhereClause(column, op, value);
            _where = newCondition;
            return (T)this;
        }

        public T Where(ICondition condition)
        {
            _where = condition;
            return (T)this;
        }

        public T AndWhere(ICondition condition)
        {
            if (_where == null)
                throw new InvalidOperationException("Cannot use AndWhere without a previous Where condition.");

            _where = new AndStatement(_where, condition);
            return (T)this;
        }

        public T AndWhere(string column, SqlOperator op, object value)
        {
            if (_where == null)
                throw new InvalidOperationException("Cannot use AndWhere without a previous Where condition.");

            var newCondition = new WhereClause(column, op, value);

            _where = new AndStatement(_where, newCondition);
            return (T)this;
        }

        public T OrWhere(ICondition condition)
        {
            if (_where == null)
                throw new InvalidOperationException("Cannot use OrWhere without a previous Where condition.");

            _where = new OrStatement(_where, condition);
            return (T)this;
        }

        public T OrWhere(string column, SqlOperator op, object value)
        {
            if (_where == null)
                throw new InvalidOperationException("Cannot use OrWhere without a previous Where condition.");

            var newCondition = new WhereClause(column, op, value);
            _where = new OrStatement(_where, newCondition);
            return (T)this;
        }

        public T Join(string table, SqlJoin join, string condition)
        {
            _joins.Add(new JoinClause
            {
                Table = table,
                JoinType = join,
                OnCondition = condition
            });

            return (T)this;
        }

        protected string BuildJoins()
        {
            StringBuilder sb = new();
            foreach (var join in _joins)
                sb.Append(' ').Append(join.ToString());
            return sb.ToString();
        }

        protected string BuildWhere()
        {
            if (_where == null) return string.Empty;
            return " WHERE " + _where.ToSql();
        }
    }
}
