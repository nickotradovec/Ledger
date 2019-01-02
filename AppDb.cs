using System;
using MySql.Data.MySqlClient;

namespace AppDatabase
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Connection;
        private string connectionString = @"Server=localhost;Port=3306;Database=ledger;Uid=testapp1;Pwd=nopassword";

        public AppDb()
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}