using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    public class AndStatement : ICondition
    {
        private readonly List<ICondition> _conditions;

        public AndStatement(ICondition left, ICondition right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            _conditions = new List<ICondition> { left, right };
        }

        public string ToSql()
        {
            return "(" + string.Join(" AND ", _conditions.Select(c => c.ToSql())) + ")";
        }
    }
}
