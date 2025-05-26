using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    public class OrStatement : ICondition
    {
        private readonly List<ICondition> _conditions;

        public OrStatement(ICondition left, ICondition right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            _conditions = new List<ICondition> { left, right };
        }

        public string ToSql()
        {
            return "(" + string.Join(" OR ", _conditions.Select(c => c.ToSql())) + ")";
        }
    }
}
