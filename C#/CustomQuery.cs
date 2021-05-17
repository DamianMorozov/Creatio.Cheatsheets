// ------------------------------------------------------------------------------------------------------------------------
// CustomQuery.
// ------------------------------------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------------------------------------
// Example 1.
// ------------------------------------------------------------------------------------------------------------------------
public void QueryExecute(UserConnection userConnection, StringBuilder queryBuilder) {
  if (queryBuilder == null)
    return;
  var query = new CustomQuery(userConnection, queryBuilder.ToString());
  query.Execute();
  queryBuilder.Clear();
}

// ------------------------------------------------------------------------------------------------------------------------
// Example 2.
// ------------------------------------------------------------------------------------------------------------------------
public void QueryExecute(UserConnection userConnection, string query) {
  if (queryBuilder == null)
    return;
  var query = new CustomQuery(userConnection, query);
  using (var dbExecutor = userConnection.EnsureDBConnection()) {
    using(var dr = query.ExecuteReader(dbExecutor)) {
      DataTable dataTable;
      while (true) {
        var queryResult = new QueryResult();
        dataTable = new DataTable();
        dataTable.Load(dr);
        queryResult.Columns = new List<string>();
        queryResult.Columns.AddRange(dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName));
        if (queryResult.Columns.Count == 0) {
          rowsAffected = (dr.RecordsAffected > 0) ? dr.RecordsAffected : 0;
          break;
        }
        queryResult.Rows = new List<List<string>>();
        foreach (DataRow row in dataTable.Rows)
        {
          var r = new List<string>();
          r.AddRange(row.ItemArray.Select(field => (field == DBNull.Value) ? "NULL" : field.ToString()));
          queryResult.Rows.Add(r);
        }
        queryResults.Add(queryResult);
      }
    }
  }
}
