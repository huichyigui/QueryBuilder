namespace QueryBuilder.Enums
{
    public enum SqlOperator
    {
        Equal,          // =
        NotEqual,       // != or <>
        GreaterThan,    // >
        LessThan,       // <
        GreaterThanOrEqual, // >=
        LessThanOrEqual,    // <=
        Like,          // LIKE
        In,            // IN
        NotIn,         // NOT IN
        IsNull,        // IS NULL
        IsNotNull      // IS NOT NULL
    }
}
