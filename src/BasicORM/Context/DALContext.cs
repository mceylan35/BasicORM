using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicORM.Context
{
    public class DALContext
    {
        private static SqlConnection _sqlConnection;
        private static object _object = new();
        
        static DALContext()
        {
            lock (_object)
                _sqlConnection = new SqlConnection("");
        }

        async Task<SqlConnection> GetOpenConnectionAsync()
        {
            if (_sqlConnection.State==System.Data.ConnectionState.Closed)
            {
                await _sqlConnection.OpenAsync();
            }
            return _sqlConnection;
        }
        public async Task CloseConnectionAsync()
        {
            if (_sqlConnection.State==System.Data.ConnectionState.Open)
            {
                await _sqlConnection.CloseAsync();
            }
        }
        public async Task<SqlDataReader> ExecuteQueryAsync(string query)
        {
            using (SqlCommand command=new SqlCommand(query,await GetOpenConnectionAsync()))
            {
                SqlDataReader reader = await command.ExecuteReaderAsync();
                return reader;
            }
        }
    }
}
