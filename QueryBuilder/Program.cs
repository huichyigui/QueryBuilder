using QueryBuilder.Builders;
using QueryBuilder.Enums;
using QueryBuilder.Models;

var insert = new InsertStatement()
                .Into("Users")
                .Values(new Dictionary<string, object>
                {
                    { "Name", "John Doe" },
                    { "Age", 30 }
                });

Console.WriteLine(insert.Build());

var select = new SelectStatement()
                .Select("Name, Age")
                .From("Users")
                .Where("Age", SqlOperator.GreaterThan, 18)
                .OrWhere("Career", SqlOperator.Equal, "Teacher")
                .OrderBy("Name")
                .Top(10)
                .Distinct();

Console.WriteLine(select.Build());

var delete = new DeleteStatement()
                .From("Users")
                .Where("Age", SqlOperator.LessThan, 18);

Console.WriteLine(delete.Build());

var update = new UpdateStatement()
                .For("Users")
                .Set("Career", "Engineer")
                .Where("Name", SqlOperator.Equal, "John Doe");

Console.WriteLine(update.Build());

var complexCondition = new OrStatement(
    new AndStatement(
        new WhereClause("Amount", SqlOperator.GreaterThan, 3),
        new WhereClause("Status", SqlOperator.Equal, "Success")
    ),
    new AndStatement(
        new WhereClause("Amount", SqlOperator.LessThan, 1),
        new OrStatement(
            new WhereClause("Category", SqlOperator.Equal, "A"),
            new WhereClause("Category", SqlOperator.Equal, "B")
        )
    )
);

var sql = new SelectStatement()
                .Select("ID, Name")
                .From("PurchaseOrder")
                .Where(complexCondition)
                .OrderBy("Name", SqlOrderBy.Asc)
                .OrderBy("Age", SqlOrderBy.Desc)
                .Build();

Console.WriteLine(sql);