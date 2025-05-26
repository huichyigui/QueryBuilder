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

        public AndStatement(params ICondition[] conditions)
        {
            _conditions = conditions.ToList();
        }

        public string ToSql()
        {
            return "(" + string.Join(" AND ", _conditions.Select(c => c.ToSql())) + ")";
        }
    }
}
