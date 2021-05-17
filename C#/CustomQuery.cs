// ------------------------------------------------------------------------------------------------------------------------
// CustomQuery.
// ------------------------------------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------------------------------------
// Example 1.
// ------------------------------------------------------------------------------------------------------------------------
public void QueryExecute(UserConnection userConnection, StringBuilder queryBuilder)
{
    if (queryBuilder == null)
        return;
    var query = new CustomQuery(userConnection, queryBuilder.ToString());
    query.Execute();
    queryBuilder.Clear();
}

// ------------------------------------------------------------------------------------------------------------------------
// Example 2.
// ------------------------------------------------------------------------------------------------------------------------
public SqlConsoleService.QueryResult QueryExecute(UserConnection userConnection, string queryString)
{
    if (string.IsNullOrEmpty(queryString))
        return null;
    var dataTable = new DataTable();
    SqlConsoleService.QueryResult queryResult = null;
    var query = new CustomQuery(userConnection, queryString);
    using (var dbExecutor = userConnection.EnsureDBConnection())
    {
        using (var dr = query.ExecuteReader(dbExecutor))
        {
            while (true)
            {
                queryResult = new SqlConsoleService.QueryResult();
                dataTable.Load(dr);
                queryResult.Columns = new List<string>();
                queryResult.Columns.AddRange(dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName));
                if (queryResult.Columns.Count == 0)
                {
                    // rowsAffected = (dr.RecordsAffected > 0) ? dr.RecordsAffected : 0;
                    break;
                }
                queryResult.Rows = new List<List<string>>();
                foreach (DataRow row in dataTable.Rows)
                {
                    var r = new List<string>();
                    r.AddRange(row.ItemArray.Select(field => (field == DBNull.Value) ? "NULL" : field.ToString()));
                    queryResult.Rows.Add(r);
                }
            }
        }
    }
    return queryResult;
}