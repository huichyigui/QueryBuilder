using QueryBuilder.Builders;
using QueryBuilder.Enums;

var insert = new InsertStatement();
insert.Into = "Users";
insert.Values(new Dictionary<string, object>
    {
        { "Name", "John Doe" },
        { "Age", 30 }
    });

Console.WriteLine(insert.Build());

var select = new SelectStatement();
select.Select = "Name, Age";
select.From = "Users";
select.Where("Age", SqlOperator.GreaterThan, 18);
select.OrWhere("Career", SqlOperator.Equal, "Teacher");
select.OrderByColumn = "Name";
select.Top = 10;

Console.WriteLine(select.Build());

var delete = new DeleteStatement();
delete.From = "Users";
delete.Where("Age", SqlOperator.LessThan, 18);

Console.WriteLine(delete.Build());

var update = new UpdateStatement();
update.Table = "Users";
update.Set("Career", "Engineer");
update .Where("Name", SqlOperator.Equal, "John Doe");

Console.WriteLine(update.Build());