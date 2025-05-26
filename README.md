 # 📦QueryBuilder
A lightweight, fluent C# library for programmatically and safely building SQL queries. Designed to standardize SQL query construction and reduce the risk of SQL injection or syntactic errors.

## 🚀 Features
- ✅ Fluent API for readable and chainable query construction  
- ✅ Support for `SELECT`, `WHERE`, `AND`, `OR`, `IN`, `JOIN`, `GROUP BY`, `ORDER BY`, `TOP`, and `DISTINCT`
- ✅ Nested conditions (`AND`, `OR`) with full control over logical grouping
- ✅ Extensible base class for future `UPDATE`, `DELETE`, `INSERT` support
- ✅ Type-safe and intuitive

## 🔧 Sample Usage
```cs
string select = new SelectStatement()
                .Select("Name, Age")
                .From("Users")
                .Where("Age", SqlOperator.GreaterThan, 18)
                .OrWhere("Career", SqlOperator.Equal, "Teacher")
                .OrderBy("Name")
                .Top(10)
                .Distinct()
                .Build();                
```
Output:
```sql
SELECT DISTINCT TOP 10 Name, Age FROM Users WHERE (Age > 18 OR Career = 'Teacher') ORDER BY Name ASC
```
## 🔍 Advanced Logic (Nested AND / OR)
```cs
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

string sql = new SelectStatement()
                .Select("ID, Name")
                .From("PurchaseOrder")
                .Where(complexCondition)
                .OrderBy("Name", SqlOrderBy.Asc)
                .OrderBy("Age", SqlOrderBy.Desc)
                .Build();
```
Output:
```sql
SELECT ID, Name FROM PurchaseOrder WHERE ((Amount > 3 AND Status = 'Success') OR (Amount < 1 AND (Category = 'A' OR Category = 'B'))) ORDER BY Name ASC, Age DESC
```

# 🙌 Contributing
Pull requests are welcome! If you'd like to add support for other query types or improve nesting logic, feel free to fork and submit a PR.
