using QueryBuilder.Enums;
using QueryBuilder.Models;
using System.Text;

namespace QueryBuilder.Builders
{
    public abstract class QueryStatementBase
    {
        protected ICondition _where;
        protected readonly List<JoinClause> _joins = new();

        public QueryStatementBase Where(string column, SqlOperator op)
        {
            return Where(column, op, null);
        }

        public QueryStatementBase Where(string column, SqlOperator op, object value)
        {
            var newCondition = new WhereClause
            {
                Column = column,
                Operator = op,
                Value = value
            };

            if (_where == null)
                _where = newCondition;
            else
                _where = new AndStatement(_where, newCondition);

            return this;
        }

        public QueryStatementBase Where(ICondition condition)
        {
            if (_where == null)
                _where = condition;
            else
                _where = new AndStatement(_where, condition);

            return this;
        }

        public QueryStatementBase AndWhere(ICondition condition)
        {
            if (_where == null)
                throw new InvalidOperationException("Cannot use AndWhere without a previous Where condition.");

            _where = new AndStatement(_where, condition);
            return this;
        }

        public QueryStatementBase AndWhere(string column, SqlOperator op, object value)
        {
            if (_where == null)
                throw new InvalidOperationException("Cannot use AndWhere without a previous Where condition.");

            var newCondition = new WhereClause
            {
                Column = column,
                Operator = op,
                Value = value
            };

            _where = new AndStatement(_where, newCondition);
            return this;
        }

        public QueryStatementBase OrWhere(ICondition condition)
        {
            if (_where == null)
                throw new InvalidOperationException("Cannot use OrWhere without a previous Where condition.");

            _where = new OrStatement(_where, condition);
            return this;
        }

        public QueryStatementBase OrWhere(string column, SqlOperator op, object value)
        {
            if (_where == null)
                throw new InvalidOperationException("Cannot use OrWhere without a previous Where condition.");

            var newCondition = new WhereClause
            {
                Column = column,
                Operator = op,
                Value = value
            };

            _where = new OrStatement(_where, newCondition);
            return this;
        }

        public QueryStatementBase Join(string table, SqlJoin join, string condition)
        {
            _joins.Add(new JoinClause
            {
                Table = table,
                JoinType = join,
                OnCondition = condition
            });

            return this;
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
