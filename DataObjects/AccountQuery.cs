using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using AppDatabase;

namespace Ledger.DataObjects
{
    public class AccountQuery
    {
        public readonly AppDb Db;
        public AccountQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = "select * from accounts";
            return await GetAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<Account>> GetAllAsync(DbDataReader reader)
        {
            var accounts = new List<Account>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Account(Db)
                    {
                        AccountId = await reader.GetFieldValueAsync<int>(0),
                        AccountName = await reader.GetFieldValueAsync<string>(3)
                    };
                    accounts.Add(post);
                }
            }
            return accounts;
        }
    }
}